using System.Collections.Generic;

namespace ChestSystem.Chests
{
    public class ChestService
    {
        private List<ChestController> activeChests = new();

        public ChestController CreateChest(ChestScriptableObject chestData, ChestView chestView)
        {
            var controller = new ChestController(chestData, chestView);
            activeChests.Add(controller);
            return controller;
        }

        public void UpdateAllChests()
        {
            foreach (var chest in activeChests)
                chest.UpdateState();
        }

        public List<ChestController> GetActiveChests() => activeChests;
    }
}