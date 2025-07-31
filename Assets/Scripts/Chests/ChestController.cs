using System;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestController
    {
        private ChestSO chestData;
        private ChestView chestView;

        public ChestController(ChestSO chestData, ChestView chestView)
        {
            this.chestData = chestData;
            this.chestView = chestView;

            chestView.Initialize(chestData);
            chestView.PlayLockAnimation();
        }

        public int CalculateUnlockCost(TimeSpan remainingTime)
        {
            int remainingMinutes = (int)Math.Ceiling(remainingTime.TotalMinutes);
            int cost = Mathf.CeilToInt(remainingMinutes / 10f);
            return Mathf.Max(cost, 1); // Always at least 1 gem
        }

        public void CollectChestRewards(out int coins, out int gems)
        {
            chestView.PlayOpenAnimation();
            GetRandomReward(out coins, out gems);
        }

        private void GetRandomReward(out int coins, out int gems)
        {
            coins = UnityEngine.Random.Range(chestData.coins.min, chestData.coins.max + 1);
            gems = UnityEngine.Random.Range(chestData.gems.min, chestData.gems.max + 1);
        }

        public string GetChestTypeText()
        {
            switch (ChestData.chestType)
            {
                case ChestType.COMMON:
                    return "Common";
                case ChestType.EPIC:
                    return "Epic";
                case ChestType.RARE:
                    return "Rare";
                case ChestType.LEGENDARY:
                    return "Legendary";
                default:
                    return null;
            }
        }

        public ChestSO ChestData => chestData;

        ~ChestController() => chestView = null;
    }
}