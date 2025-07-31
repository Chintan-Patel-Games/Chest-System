using ChestSystem.Main;
using ChestSystem.Sound;

namespace ChestSystem.UI.ExitGamePopupUI
{
    public class ExitGamePopupUIController : IUIController
    {
        private ExitGamePopupUIView view;

        public ExitGamePopupUIController(ExitGamePopupUIView view)
        {
            this.view = view;
            view.SetController(this);
            Hide();
        }

        public void OnYesButtonClicked()
        {
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.UI_BUTTON_CLICK);
            GameService.Instance.OnExitGame();
            Hide();
        }

        public void OnExitGameClicked()
        {
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.UI_BUTTON_CLICK);
            Hide();
        }

        public void Show()
        {
            view.EnableView();
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.UI_POPUP_OPEN);
        }

        public void Hide() => view.DisableView();
    }
}