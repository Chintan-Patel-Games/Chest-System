using ChestSystem.Chests.ChestSlot;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestService
    {
        private Dictionary<ChestType, ChestScriptableObject> chestDatabase;
        private ChestSlotPool slotPool;
        private ChestView chestPrefab;

        public ChestService(ChestSlotView chestSlotPrefab, Transform slotPoolTransform, ChestView chestPrefab)
        {
            slotPool = new ChestSlotPool(chestSlotPrefab, slotPoolTransform);
            this.chestPrefab = chestPrefab;

            LoadChestDatabase();
        }

        private void LoadChestDatabase()
        {
            chestDatabase = new Dictionary<ChestType, ChestScriptableObject>();
            var allSO = Resources.LoadAll<ChestScriptableObject>("Chests"); // Put all ChestSO in Resources/ChestData
            foreach (var so in allSO)
                chestDatabase[so.chestType] = so;
        }

        public void GenerateChest()
        {
            var emptySlot = slotPool.GetFirstEmptySlot();
            if (emptySlot == null)
            {
                Debug.LogWarning("No empty chest slot available!");
                return;
            }

            // Randomly pick a chest type
            ChestType randomType = GetRandomChestType();
            ChestScriptableObject chestData = chestDatabase[randomType];

            // Assign to slot
            emptySlot.AssignChest(chestData, chestPrefab);
        }

        private ChestType GetRandomChestType()
        {
            var values = (ChestType[])System.Enum.GetValues(typeof(ChestType));
            return values[Random.Range(0, values.Length)];
        }
    }
}