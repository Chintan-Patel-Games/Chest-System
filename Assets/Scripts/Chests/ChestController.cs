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

        public void CollectChest()
        {
            chestView.PlayOpenAnimation();
            // Grant rewards, etc.
        }
        public int CalculateGemCost(TimeSpan remainingTime)
        {
            int remainingMinutes = (int)Math.Ceiling(remainingTime.TotalMinutes);
            int cost = Mathf.CeilToInt(remainingMinutes / 10f);
            return Mathf.Max(cost, 1); // Always at least 1 gem
        }

        public ChestSO GetChestData() => chestData;

        public ChestView GetChestView() => chestView;

        ~ChestController() => chestView = null;
    }
}