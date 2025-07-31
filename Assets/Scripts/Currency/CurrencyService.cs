namespace ChestSystem.Currency
{
    public class CurrencyService
    {
        private CurrencyController currencyController;

        public void Initialize(int initialCoins, int initialGems)
        {
            currencyController = new CurrencyController();
            currencyController.Initialize(initialCoins, initialGems);
        }

        public void AddCoins(int amount) => currencyController.AddCoins(amount);
        public void AddGems(int amount) => currencyController.AddGems(amount);
        public void RemoveCoins(int amount) => currencyController.RemoveCoins(amount);
        public void RemoveGems(int amount) => currencyController.RemoveGems(amount);

        public int GetTotalCoins() => currencyController.Coins;
        public int GetTotalGems() => currencyController.Gems;
    }
}