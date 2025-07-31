using System.Collections.Generic;
using ChestSystem.Chests.ChestSlot;
using ChestSystem.Main;

namespace ChestSystem.Chests.ChestUnlockQueue
{
    public class ChestUnlockQueueController
    {
        private Queue<ChestSlotController> queuedChests = new();
        private ChestSlotController currentlyUnlockingChest;

        public ChestUnlockQueueController()
        {
            ResetQueue();
            SubscribeToEvents();
        }

        public void SubscribeToEvents() => GameService.Instance.EventService.OnChestReadyToOpen.AddListener(OnChestUnlockComplete);

        public void UnSubscribeToEvents() => GameService.Instance.EventService.OnChestReadyToOpen.RemoveListener(OnChestUnlockComplete);

        public void TryUnlockChest(ChestSlotController controller)
        {
            if (currentlyUnlockingChest == null)
            {
                currentlyUnlockingChest = controller;
                controller.StartUnlocking(controller);
            }
            else
                queuedChests.Enqueue(controller);
        }

        private void OnChestUnlockComplete(ChestSlotController unlockedChest)
        {
            if (currentlyUnlockingChest == unlockedChest)
                currentlyUnlockingChest = null;

            if (queuedChests.Count > 0)
            {
                var nextChest = queuedChests.Dequeue();
                currentlyUnlockingChest = nextChest;
                nextChest.StartUnlocking(unlockedChest);
            }
        }

        private void ResetQueue()
        {
            queuedChests.Clear();
            currentlyUnlockingChest = null;
        }

        public bool IsAnyChestUnlocking() => currentlyUnlockingChest != null;

        public bool IsChestAlreadyInQueue(ChestSlotController controller) => queuedChests.Contains(controller);

        ~ChestUnlockQueueController() => UnSubscribeToEvents();
    }
}