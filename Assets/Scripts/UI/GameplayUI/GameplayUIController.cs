using ChestSystem.Main;
using ChestSystem.Sound;

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

        public void OnGenerateChestClicked()
        {
            GameService.Instance.EventService.OnGenerateChest.InvokeEvent();
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.UI_BUTTON_CLICK);
        }

        public void OnExitGameClicked()
        {
            GameService.Instance.UIService.ShowExitPopupUI();
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.UI_BUTTON_CLICK);
        }

        public void UpdateCoinsText(int totalCoins) => view.UpdateCoinsText(totalCoins);

        public void UpdateGemsText(int totalGems) => view.UpdateGemsText(totalGems);

        public void Show() => view.EnableView();

        public void Hide() => view.DisableView();
    }
}