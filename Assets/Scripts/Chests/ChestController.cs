using ChestSystem.Chests.States;
using ChestSystem.Chests.States.ConcreateStates;
using ChestSystem.Main;
using System;

namespace ChestSystem.Chests
{
    public class ChestController
    {
        private ChestBaseState currentState;
        private ChestScriptableObject chestData;
        private ChestView chestView;
        private DateTime unlockStartTime;

        public ChestController(ChestScriptableObject chestData, ChestView chestView)
        {
            this.chestData = chestData;
            this.chestView = chestView;

            chestView.Initialize(chestData);
            chestView.ResetToLocked();
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

        public void NotifyChestReady(Object obj) => GameService.Instance.EventService.OnChestReadyToOpen.InvokeEvent(null);

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

        public ChestScriptableObject GetChestData() => chestData;

        public ChestView GetChestPrefab() => chestView;
    }
}