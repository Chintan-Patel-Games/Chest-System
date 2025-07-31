using ChestSystem.Chests.ChestSlot;
using ChestSystem.Main;

namespace ChestSystem.UI.PopupUI
{
    public class PopupUIController : IUIController
    {
        private PopupUIView view;
        private ChestSlotController targetSlot;

        public PopupUIController(PopupUIView view)
        {
            this.view = view;
            view.SetController(this);
            Hide();
        }

        public void SetTargetSlot(ChestSlotController controller) => targetSlot = controller;

        public void OnUnlockWithTimerClicked()
        {
            GameService.Instance.EventService.OnUnlockWithTimer.InvokeEvent(targetSlot);
            Hide();
        }

        public void OnUnlockWithGemsClicked()
        {
            GameService.Instance.EventService.OnUnlockWithGems.InvokeEvent(targetSlot);
            Hide();
        }

        public void Show() => view.EnableView();

        public void Hide() => view.DisableView();
    }
}