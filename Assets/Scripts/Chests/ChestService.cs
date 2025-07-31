using ChestSystem.Chests.ChestSlot;
using ChestSystem.Main;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestService
    {
        private List<ChestSO> chestDatabase;
        private ChestSlotPool chestSlotPool;

        public ChestService()
        {
            LoadChestDatabase();
            SubscribeToEvents();
        }

        public void Initialize() => chestSlotPool = new ChestSlotPool();

        private void SubscribeToEvents() => GameService.Instance.EventService.OnGenerateChest.AddListener(GenerateChest);

        private void UnSubscribeToEvents() => GameService.Instance.EventService.OnGenerateChest.RemoveListener(GenerateChest);

        private void LoadChestDatabase() => chestDatabase = new List<ChestSO>(Resources.LoadAll<ChestSO>("Chests"));

        private void GenerateChest() => chestSlotPool.AssignChest(chestDatabase);

        ~ChestService()
        {
            UnSubscribeToEvents();
            chestDatabase.Clear();
        }
    }
}