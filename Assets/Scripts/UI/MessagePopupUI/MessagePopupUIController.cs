using ChestSystem.Main;

namespace ChestSystem.UI.MessagePopupUI
{
    public class MessagePopupUIController : IUIController
    {
        private MessagePopupUIView view;

        public MessagePopupUIController(MessagePopupUIView view)
        {
            this.view = view;
            view.SetController(this);
            Hide();
        }

        public void SetMessageText(string message)
        {
            view.SetMessageText(message);
            Show();
        }

        public void OnClosePopup()
        {
            UnlockRaycastBlock();
            Hide();
        }

        private void UnlockRaycastBlock() => GameService.Instance.UIService.SetUIRaycastBlock(true);

        public void Show() => view.EnableView();

        public void Hide() => view.DisableView();
    }
}