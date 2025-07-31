using ChestSystem.Chests.ChestSlot;
using ChestSystem.Main;

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
            UnlockRaycastBlock();
            Hide();
        }

        public void OnCollectRewardsClicked()
        {
            GameService.Instance.EventService.OnCollectChest.InvokeEvent(targetSlot);
            UnlockRaycastBlock();
            Hide();
        }

        public void OnCloseButtonClicked()
        {
            UnlockRaycastBlock();
            Hide();
        }

        private void UnlockRaycastBlock() => GameService.Instance.UIService.SetUIRaycastBlock(true);

        public void Show() => view.EnableView();

        public void Hide() => view.DisableView();
    }
}