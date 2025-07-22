using ChestSystem.Chests.States;
using ChestSystem.Chests.States.ConcreateStates;
using System;

namespace ChestSystem.Chests
{
    public class ChestController
    {
        private ChestBaseState currentState;
        private ChestScriptableObject chestData;
        private ChestView chestView;
        private DateTime unlockStartTime;

        public ChestController(ChestScriptableObject data, ChestView view)
        {
            chestData = data;
            chestView = view;
            SetState(new LockedChestState(this));
        }

        public void SetState(ChestBaseState newState)
        {
            currentState = newState;
            currentState.EnterState();
        }

        public void OnChestClicked() => currentState.OnChestClicked();

        public void UpdateState() => currentState.UpdateState();

        public void StartUnlocking()
        {
            unlockStartTime = DateTime.UtcNow;
            SetState(new UnlockingChestState(this));
        }

        public void CollectChest()
        {
            chestView.PlayOpenAnimation();
            // Grant rewards, etc.
            SetState(new OpenedChestState(this));
        }

        public bool HasUnlockTimePassed()
        {
            var elapsed = DateTime.UtcNow - unlockStartTime;
            return elapsed.TotalMinutes >= ConvertUnlockTimerinMinutes(chestData.unlockDurationMinutes);
        }

        private int ConvertUnlockTimerinMinutes(ChestUnlockTimer chestUnlockTimer) => (int)chestUnlockTimer;
    }
}