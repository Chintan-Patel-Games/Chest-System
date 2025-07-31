namespace ChestSystem.UI.RewardsPopupUI
{
    public class RewardsPopupUIController : IUIController
    {
        private RewardsPopupUIView view;

        public RewardsPopupUIController(RewardsPopupUIView view)
        {
            this.view = view;
            view.SetController(this);
            Hide();
        }

        public void SetRewards(int coins, int gems)
        {
            view.SetCoinsText(coins);
            view.SetGemsText(gems);
        }

        public void Show() => view.EnableView();

        public void Hide() => view.DisableView();
    }
}