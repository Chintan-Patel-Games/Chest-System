using ChestSystem.Chests.ChestSlot;
using ChestSystem.Main;
using ChestSystem.Sound;
using ChestSystem.Utilities;

namespace ChestSystem.UI.UnlockWithGemsPopupUI
{
    public class UnlockWithGemsPopupUIController : IUIController
    {
        private UnlockwithGemsPopupUIView view;
        private ChestSlotController targetSlot;
        private int unlockCost;

        public UnlockWithGemsPopupUIController(UnlockwithGemsPopupUIView view)
        {
            this.view = view;
            view.SetController(this);
            Hide();
        }

        public void SetTargetSlot(ChestSlotController controller) => targetSlot = controller;

        public void OnUnlockWithGemsClicked()
        {
            if (targetSlot.CalculateUnlockCost() > GameService.Instance.CurrencyService.GetTotalGems())
            {
                GameService.Instance.UIService.ShowWarningPopupUI(StringConstants.NotEnoughGemsWarning);
                return;
            }

            GameService.Instance.EventService.OnUnlockWithGems.InvokeEvent(targetSlot);
            GameService.Instance.EventService.OnChestReadyToOpen.InvokeEvent(targetSlot);
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