using System.Collections.Generic;

namespace ChestSystem.Chests
{
    public class ChestService
    {
        private List<ChestController> activeChests = new List<ChestController>();

        public ChestController CreateChest(ChestScriptableObject chestData)
        {
            var controller = new ChestController(chestData);
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