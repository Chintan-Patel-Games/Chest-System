using ChestSystem.Chests.ChestSlot;
using ChestSystem.Main;
using ChestSystem.Sound;

namespace ChestSystem.UI.RewardsPopupUI
{
    public class RewardsPopupUIController : IUIController
    {
        private RewardsPopupUIView view;
        private ChestSlotController targetSlot;

        public RewardsPopupUIController(RewardsPopupUIView view)
        {
            this.view = view;
            view.SetController(this);
            Hide();
        }

        public void SetTargetSlot(ChestSlotController controller) => targetSlot = controller;

        public void SetRewards(int totalCoins, int totalGems) => view.SetCurrencyText(totalCoins, totalGems);

        public void OnCloseButtonClicked()
        {
            GameService.Instance.EventService.OnChestRemove.InvokeEvent(targetSlot);
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.UI_POPUP_CLOSE);
            UnlockRaycastBlock();
            Hide();
        }

        private void UnlockRaycastBlock() => GameService.Instance.UIService.SetUIRaycastBlock(true);

        public void Show()
        {
            view.EnableView();
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.UI_POPUP_OPEN);
        }

        public void Hide() => view.DisableView();
    }
}