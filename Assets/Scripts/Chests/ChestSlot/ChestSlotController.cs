using ChestSystem.Chests.States;
using ChestSystem.Chests.States.ConcreateStates;
using ChestSystem.Command;
using ChestSystem.Main;
using System;
using UnityEngine;

namespace ChestSystem.Chests.ChestSlot
{
    public class ChestSlotController
    {
        private DateTime unlockStartTime;

        private ChestSlotView view;

        private ChestBaseState currentState;
        private ChestController chestController;
        private ChestSO chestData;

        public ChestSlotController(ChestSlotView view)
        {
            this.view = view;
            view.SetController(this);
            SetState(new EmptyChestState(this));
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.EventService.OnUnlockWithTimer.AddListener(StartUnlocking);
            GameService.Instance.EventService.OnUnlockWithGems.AddListener(FinishUnlocking);
            GameService.Instance.EventService.OnUnlockWithGems.AddListener(RegisterUndoCommand);
            GameService.Instance.EventService.OnChestRemove.AddListener(RemoveChest);
            GameService.Instance.EventService.OnCollectChest.AddListener(CollectChest);
        }

        private void UnSubscribeToEvents()
        {
            GameService.Instance.EventService.OnUnlockWithTimer.RemoveListener(StartUnlocking);
            GameService.Instance.EventService.OnUnlockWithGems.RemoveListener(FinishUnlocking);
            GameService.Instance.EventService.OnUnlockWithGems.RemoveListener(RegisterUndoCommand);
            GameService.Instance.EventService.OnChestRemove.RemoveListener(RemoveChest);
            GameService.Instance.EventService.OnCollectChest.RemoveListener(CollectChest);
        }

        public void AssignChest(ChestSO chestSO)
        {
            ChestSlotView createdView = view.CreateChest(chestSO);
            chestController = createdView.GetChestController();
            chestData = chestSO;

            SetState(new LockedChestState(this));
        }

        public void OnSlotClicked()
        {
            currentState.OnChestClicked();
            Debug.Log("Chest Slot Clicked: " + currentState);
        }

        public void UpdateState() => currentState.UpdateState();

        public void SlotEmptyState() => view.SlotEmptyState();

        public void SlotLockedState() => view.SlotLockedState();

        public void SlotUnlockingState() => view.SlotUnlockingState();

        public void SlotUnlockedState() => view.SlotUnlockedState();

        public void StartUnlocking(ChestSlotController target)
        {
            if (target == this && target.currentState is LockedChestState)
            {
                unlockStartTime = DateTime.UtcNow;
                SetState(new UnlockingChestState(this));
                StartTimer();
            }
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

        public void ResetTimer() => view.ResetTimer();

        private void StartTimer()
        {
            TimeSpan remainingTime = GetRemainingUnlockTime();

            if (remainingTime.TotalSeconds <= 0)
            {
                FinishUnlocking(this);
                return;
            }

            view.StartUnlockTimer(GetRemainingUnlockTime, () => FinishUnlocking(this));
        }

        public void FinishUnlocking(ChestSlotController target)
        {
            if (target == this)
            {
                view.StopUnlockTimer();
                SetState(new UnlockedChestState(this));
            }
        }

        public void NotifyChestReady() => GameService.Instance.EventService.OnChestReadyToOpen.InvokeEvent();

        public void CollectChest(ChestSlotController target)
        {
            if (target == this && target.currentState is UnlockedChestState)
            {
                GetRandomReward(out int totalCoins, out int totalGems);
                GameService.Instance.UIService.ShowRewardsPopupUI(totalCoins, totalGems);
                GameService.Instance.UIService.SetTargetSlotForRemove(this);
                GameService.Instance.CurrencyService.AddCoins(totalCoins);
                GameService.Instance.CurrencyService.AddGems(totalGems);
            }
        }

        private void GetRandomReward(out int coins, out int gems)
        {
            coins = UnityEngine.Random.Range(chestData.coins.min, chestData.coins.max + 1);
            gems = UnityEngine.Random.Range(chestData.gems.min, chestData.gems.max + 1);
        }

        public void RemoveChest(ChestSlotController target)
        {
            if (target == this)
            {
                view.RemoveChestView();
                chestData = null;
                SetState(new EmptyChestState(this));
            }
        }

        public TimeSpan GetRemainingUnlockTime()
        {
            var duration = TimeSpan.FromMinutes((int)chestData.unlockDurationMinutes);
            if (unlockStartTime == default)
                return duration; // Assume full time if timer hasn't started

            var elapsed = DateTime.UtcNow - unlockStartTime;
            var remaining = duration - elapsed;

            return remaining > TimeSpan.Zero ? remaining : TimeSpan.Zero;
        }

        public bool HasUnlockTimePassed()
        {
            var elapsed = DateTime.UtcNow - unlockStartTime;
            return elapsed.TotalMinutes >= ConvertUnlockTimerinMinutes(chestData.unlockDurationMinutes);
        }

        private int ConvertUnlockTimerinMinutes(ChestUnlockTimer chestUnlockTimer) => (int)chestUnlockTimer;

        public int CalculateUnlockCost() => chestController.CalculateGemCost(GetRemainingUnlockTime());

        public void SetState(ChestBaseState newState)
        {
            Debug.Log("Current State: " + currentState);
            currentState = newState;
            currentState.EnterState();
            Debug.Log("New State: " + currentState);
        }

        public ChestBaseState GetCurrentState() => currentState;

        public bool IsEmpty() => currentState is EmptyChestState;

        public void RevertToState(ChestBaseState previousState, TimeSpan remainingTime)
        {
            if (previousState is UnlockingChestState)
            {
                SlotLockedState();
                SetState(new LockedChestState(this));
                remainingTime = GetRemainingUnlockTime();
            }
        }

        ~ChestSlotController() => UnSubscribeToEvents();
    }
}