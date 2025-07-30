using ChestSystem.Main;
using UnityEngine;

namespace ChestSystem.Currency
{
    public class CurrencyController
    {
        private int coins;
        private int gems;

        public void Initialize(int initialCoins, int initialGems)
        {
            coins = initialCoins;
            gems = initialGems;
            GameService.Instance.UIService.UpdateCoinsText(coins);
            GameService.Instance.UIService.UpdateGemsText(gems);
        }

        public void AddCoins(int amount)
        {
            if (amount < 0) return;
            coins += amount;
            GameService.Instance.UIService.UpdateCoinsText(coins);
        }

        public void AddGems(int amount)
        {
            if (amount < 0) return;
            gems += amount;
            GameService.Instance.UIService.UpdateGemsText(gems);
        }

        public void RemoveCoins(int amount)
        {
            if (amount < 0) return;
            coins = Mathf.Max(0, coins - amount);
            GameService.Instance.UIService.UpdateCoinsText(coins);
        }

        public void RemoveGems(int amount)
        {
            if (amount < 0) return;
            gems = Mathf.Max(0, gems - amount);
            GameService.Instance.UIService.UpdateGemsText(gems);
        }

        public int Coins => coins;
        public int Gems => gems;
    }
}