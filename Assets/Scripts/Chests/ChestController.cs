using ChestSystem.Utilities;
using System;

namespace ChestSystem.Chests
{
    public class ChestController
    {
        private ChestScriptableObject chestData;
        private ChestState currentState;
        private DateTime unlockStartTime;

        public ChestController(ChestScriptableObject data)
        {
            chestData = data;
            currentState = ChestState.LOCKED;
        }

        public void OnChestClicked()
        {
            switch (currentState)
            {
                case ChestState.LOCKED:
                    StartUnlocking();
                    break;
                case ChestState.UNLOCKED:
                    CollectChest();
                    break;
            }
        }

        public void StartUnlocking()
        {
            if (currentState != ChestState.LOCKED) return;

            currentState = ChestState.UNLOCKING;
            unlockStartTime = DateTime.UtcNow;
        }

        public void CollectChest()
        {
            if (currentState == ChestState.UNLOCKED)
            {
                currentState = ChestState.OPENED;
                // TODO: Grant rewards
            }
        }

        public void UpdateState()
        {
            if (currentState == ChestState.UNLOCKING)
            {
                TimeSpan elapsed = DateTime.UtcNow - unlockStartTime;
                if (elapsed.TotalMinutes >= ConvertUnlockTimerinMinutes(chestData.unlockDurationMinutes))
                    currentState = ChestState.UNLOCKED;
            }
        }

        public string GetRemainingTimeFormatted()
        {
            TimeSpan remaining = TimeSpan.FromMinutes(ConvertUnlockTimerinMinutes(chestData.unlockDurationMinutes)) - (DateTime.UtcNow - unlockStartTime);
            if (remaining.TotalSeconds < 0) return StringConstants.ZeroTimer;
            return StringConstants.GetFormattedRemainingTime(remaining);
        }

        public ChestScriptableObject GetChestData() => chestData;

        public ChestState GetChestState() => currentState;

        private int ConvertUnlockTimerinMinutes(ChestUnlockTimer chestUnlockTimer) => (int)chestUnlockTimer;
    }
}