using ChestSystem.Chests.ChestSlot;
using ChestSystem.Main;
using ChestSystem.Sound;
using ChestSystem.Utilities;

namespace ChestSystem.UI.UnlockChestPopupUI
{
    public class UnlockChestPopupUIController : IUIController
    {
        private UnlockChestPopupUIView view;
        private ChestSlotController targetSlot;
        private int unlockCost;

        public UnlockChestPopupUIController(UnlockChestPopupUIView view)
        {
            this.view = view;
            view.SetController(this);
            Hide();
        }

        public void SetTargetSlot(ChestSlotController controller) => targetSlot = controller;

        public void OnUnlockWithTimerClicked()
        {
            if (GameService.Instance.ChestUnlockQueueService.IsChestAlreadyInQueue(targetSlot))
            {
                GameService.Instance.UIService.ShowWarningPopupUI(StringConstants.ChestAlreadyInQueue);
                return;
            }

            if (GameService.Instance.ChestUnlockQueueService.IsAnyChestUnlocking())
                GameService.Instance.UIService.ShowMessagePopupUI(StringConstants.ChestAddedInQueue);

            GameService.Instance.EventService.OnUnlockWithTimer.InvokeEvent(targetSlot);
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.UI_BUTTON_CLICK);
            UnlockRaycastBlock();
            Hide();
        }

        public void OnUnlockWithGemsClicked()
        {
            if (targetSlot.CalculateUnlockCost() > GameService.Instance.CurrencyService.GetTotalGems())
            {
                GameService.Instance.UIService.ShowWarningPopupUI(StringConstants.NotEnoughGemsWarning);
                return;
            }

            GameService.Instance.EventService.OnUnlockWithGems.InvokeEvent(targetSlot);
            GameService.Instance.CurrencyService.RemoveGems(unlockCost);
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.UI_BUTTON_CLICK);
            UnlockRaycastBlock();
            Hide();
        }

        public void SetGemsText(int totalGems) => view.SetGemsText(totalGems);

        public void OnCloseButtonClicked()
        {
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.UI_POPUP_CLOSE);
            UnlockRaycastBlock();
            Hide();
        }

        private void UnlockRaycastBlock() => GameService.Instance.UIService.SetUIRaycastBlock(true);

        public void Show(int unlockCost)
        {
            this.unlockCost = unlockCost;
            SetGemsText(unlockCost);
            view.EnableView();
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.UI_POPUP_OPEN);
        }

        public void Hide() => view.DisableView();
    }
}