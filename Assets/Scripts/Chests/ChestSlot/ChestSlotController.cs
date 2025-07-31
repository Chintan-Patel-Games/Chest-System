using ChestSystem.Chests.States;
using ChestSystem.Chests.States.ConcreateStates;
using ChestSystem.Command;
using ChestSystem.Main;
using ChestSystem.Sound;
using System;

namespace ChestSystem.Chests.ChestSlot
{
    public class ChestSlotController
    {
        private ChestSlotView view;
        private ChestBaseState currentState;
        private ChestController chestController;
        private DateTime unlockStartTime;

        public ChestSlotController()
        {
            view = GameService.Instance.UIService.AddNewSlot();
            view.SubscribeToButtons(this);
            SetState(new EmptyChestState(this));
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.EventService.OnUnlockWithTimer.AddListener(TryUnlockChest);
            GameService.Instance.EventService.OnUnlockWithGems.AddListener(FinishUnlocking);
            GameService.Instance.EventService.OnUnlockWithGems.AddListener(RegisterUndoCommand);
            GameService.Instance.EventService.OnChestRemove.AddListener(RemoveChest);
            GameService.Instance.EventService.OnCollectChest.AddListener(CollectChest);
        }

        private void UnSubscribeToEvents()
        {
            GameService.Instance.EventService.OnUnlockWithTimer.RemoveListener(TryUnlockChest);
            GameService.Instance.EventService.OnUnlockWithGems.RemoveListener(FinishUnlocking);
            GameService.Instance.EventService.OnUnlockWithGems.RemoveListener(RegisterUndoCommand);
            GameService.Instance.EventService.OnChestRemove.RemoveListener(RemoveChest);
            GameService.Instance.EventService.OnCollectChest.RemoveListener(CollectChest);
        }

        public void AssignChest(ChestSO chestSO)
        {
            view.ActivateChestView(chestSO.chestType);
            chestController = new ChestController(chestSO, view.ChestView);

            SetState(new LockedChestState(this));
        }

        // Setting Slot States
        public void SlotEmptyState() => view.SetEmptyStateView();

        public void SlotLockedState()
        {
            int totalMinutes = (int)chestController.ChestData.unlockDurationMinutes;
            string chestTypeText = chestController.GetChestTypeText();
            view.SetLockedStateView(totalMinutes, chestTypeText);
        }

        public void SlotUnlockingState() => view.SetUnlockingStateView();

        public void SlotUnlockedState() => view.SetUnlockedStateView();

        public void OnSlotClicked() => currentState.OnSlotClicked();

        public void UpdateState() => currentState.UpdateState();

        public void TryUnlockChest(ChestSlotController target)
        {
            if (target == this)
                GameService.Instance.ChestUnlockQueueService.TryUnlockChest(this);
        }

        public void StartUnlocking(ChestSlotController target)
        {
            SetState(new UnlockingChestState(this));
            StartTimer();
        }

        public void RegisterUndoCommand(ChestSlotController target)
        {
            if (target == this)
            {
                var gemsSpent = CalculateUnlockCost();
                var commandData = new CommandData(this, gemsSpent);
                var unlockCommand = new UnlockWithGemsCommand(commandData);

                GameService.Instance.CommandInvoker.RegisterCommand(unlockCommand);
            }
        }

        public void ResetTimer() => view.ResetTimer((int)chestController.ChestData.unlockDurationMinutes);

        private void StartTimer()
        {
            unlockStartTime = DateTime.UtcNow;
            TimeSpan remainingTime = GetRemainingUnlockTime();

            if (remainingTime.TotalSeconds <= 0)
            {
                FinishUnlocking(this);
                return;
            }

            view.StartUnlockTimer(GetRemainingUnlockTime, () => chestController.CalculateUnlockCost(remainingTime), () => FinishUnlocking(this));
        }

        public void FinishUnlocking(ChestSlotController target)
        {
            if (target == this)
            {
                view.StopUnlockTimer();
                SetState(new UnlockedChestState(this));
                GameService.Instance.SoundService.PlaySoundEffects(SoundType.CHEST_UNLOCKED);
            }
        }

        public void CollectChest(ChestSlotController target)
        {
            if (target == this && target.currentState is UnlockedChestState)
            {
                chestController.CollectChestRewards(out int totalCoins, out int totalGems);
                GameService.Instance.UIService.SetTargetSlotForRewards(this);
                GameService.Instance.UIService.ShowRewardsPopupUI(totalCoins, totalGems);
                GameService.Instance.CurrencyService.AddCoins(totalCoins);
                GameService.Instance.CurrencyService.AddGems(totalGems);
                GameService.Instance.SoundService.PlaySoundEffects(SoundType.CHEST_OPEN);
            }
        }

        public void RemoveChest(ChestSlotController target)
        {
            if (target == this)
            {
                view.DeactivateChestView();
                SetState(new EmptyChestState(this));
            }
        }

        public TimeSpan GetRemainingUnlockTime()
        {
            var duration = TimeSpan.FromMinutes((int)chestController.ChestData.unlockDurationMinutes);
            if (unlockStartTime == default) return duration; // Assume full time if timer hasn't started

            var elapsed = DateTime.UtcNow - unlockStartTime;
            var remaining = duration - elapsed;

            return remaining > TimeSpan.Zero ? remaining : TimeSpan.Zero;
        }

        public bool HasUnlockTimePassed()
        {
            var elapsed = DateTime.UtcNow - unlockStartTime;
            return elapsed.TotalMinutes >= ConvertUnlockTimerinMinutes(chestController.ChestData.unlockDurationMinutes);
        }

        private int ConvertUnlockTimerinMinutes(ChestUnlockTimer chestUnlockTimer) => (int)chestUnlockTimer;

        public int CalculateUnlockCost() => chestController.CalculateUnlockCost(GetRemainingUnlockTime());

        public void SetState(ChestBaseState newState)
        {
            currentState = newState;
            currentState.EnterState();
        }

        public ChestBaseState GetCurrentState() => currentState;

        public bool IsSlotEmpty() => currentState is EmptyChestState;

        ~ChestSlotController() => UnSubscribeToEvents();
    }
}