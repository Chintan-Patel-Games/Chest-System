using ChestSystem.Main;

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

        public void OnYesButtonClicked() => GameService.Instance.OnExitGame();

        public void OnExitGameClicked() => Hide();

        public void Show() => view.EnableView();

        public void Hide() => view.DisableView();
    }
}