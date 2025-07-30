using ChestSystem.Chests.ChestSlot;
using ChestSystem.Main;
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
            GameService.Instance.EventService.OnUnlockWithTimer.InvokeEvent(targetSlot);
            UnlockRaycastBlock();
            Hide();
        }

        public void OnUnlockWithGemsClicked()
        {
            if (targetSlot.CalculateUnlockCost() > GameService.Instance.CurrencyService.GetTotalGems())
            {
                GameService.Instance.UIService.ShowMessagePopupUI(StringConstants.NotEnoughGemsWarning);
                return;
            }

            GameService.Instance.EventService.OnUnlockWithGems.InvokeEvent(targetSlot);
            GameService.Instance.CurrencyService.RemoveGems(unlockCost);
            UnlockRaycastBlock();
            Hide();
        }

        public void ShowUnlockWithChestUI(int unlockCost)
        {
            view.ShowUnlockWithGemsUI();
            Show(unlockCost);
        }

        public void SetGemsText(int totalGems) => view.SetGemsText(totalGems);

        public void OnCloseButtonClicked()
        {
            UnlockRaycastBlock();
            Hide();
        }

        private void UnlockRaycastBlock() => GameService.Instance.UIService.SetUIRaycastBlock(true);

        public void Show(int unlockCost)
        {
            this.unlockCost = unlockCost;
            SetGemsText(unlockCost);
            view.EnableView();
        }

        public void Hide() => view.DisableView();
    }
}