using ChestSystem.Chests.ChestSlot;

namespace ChestSystem.Chests.ChestUnlockQueue
{
    public class ChestUnlockQueueService
    {
        private ChestUnlockQueueController controller;

        public ChestUnlockQueueService() => controller = new ChestUnlockQueueController();

        public void TryUnlockChest(ChestSlotController chest) => controller.TryUnlockChest(chest);

        public bool IsAnyChestUnlocking() => controller.IsAnyChestUnlocking();

        public bool IsChestAlreadyInQueue(ChestSlotController slotController) => controller.IsChestAlreadyInQueue(slotController);
    }
}