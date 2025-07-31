using ChestSystem.Chests.ChestSlot;
using ChestSystem.Main;
using ChestSystem.Sound;

namespace ChestSystem.UI.CollectChestPopupUI
{
    public class CollectChestPopupUIController : IUIController
    {
        private CollectChestPopupUIView view;
        private ChestSlotController targetSlot;

        public CollectChestPopupUIController(CollectChestPopupUIView view)
        {
            this.view = view;
            view.SetController(this);
            Hide();
        }

        public void SetTargetSlot(ChestSlotController controller) => targetSlot = controller;

        public void OnUndoUnlockClicked()
        {
            GameService.Instance.CommandInvoker.Undo();
            GameService.Instance.EventService.OnUndo.InvokeEvent(targetSlot);
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.UI_BUTTON_CLICK);
            UnlockRaycastBlock();
            Hide();
        }

        public void OnCollectRewardsClicked()
        {
            GameService.Instance.EventService.OnCollectChest.InvokeEvent(targetSlot);
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.UI_BUTTON_CLICK);
            UnlockRaycastBlock();
            Hide();
        }

        public void OnCloseButtonClicked()
        {
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