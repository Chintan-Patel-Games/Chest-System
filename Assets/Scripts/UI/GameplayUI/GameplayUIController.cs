using ChestSystem.Main;

namespace ChestSystem.UI.GameplayUI
{
    public class GameplayUIController : IUIController
    {
        private GameplayUIView view;

        public GameplayUIController(GameplayUIView view)
        {
            this.view = view;
            view.SetController(this);
            Show();
        }

        public void OnGenerateChestClicked() => GameService.Instance.EventService.OnGenerateChest.InvokeEvent();

        public void Show() => view.EnableView();

        public void Hide() => view.DisableView();
    }
}