using ChestSystem.Main;

namespace ChestSystem.UI
{
    public class GameplayUIController : IUIController
    {
        private GameplayUIView gameplayView;

        public GameplayUIController(GameplayUIView gameplayView)
        {
            this.gameplayView = gameplayView;
            gameplayView.SetController(this);
            Show();
        }

        public void OnGenerateChestClicked() => GameService.Instance.ChestService.GenerateChest();

        public void Show() => gameplayView.EnableView();

        public void Hide() => gameplayView.DisableView();
    }
}