using ChestSystem.Main;
using ChestSystem.Sound;

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

        public void SetMessageText(string message) => view.SetMessageText(message);

        public void OnClosePopup()
        {
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.UI_POPUP_CLOSE);
            UnlockRaycastBlock();
            Hide();
        }

        private void UnlockRaycastBlock() => GameService.Instance.UIService.SetUIRaycastBlock(true);

        public void ShowWarningPopup(string message)
        {
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.UI_WARNING_POPUP_OPEN);
            view.EnableWarningView();
            SetMessageText(message);
        }

        public void ShowMessagePopup(string message)
        {
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.UI_POPUP_OPEN);
            view.EnableMessageView();
            SetMessageText(message);
        }

        public void Hide() => view.DisableView();
    }
}