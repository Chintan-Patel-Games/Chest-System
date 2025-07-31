using ChestSystem.Chests.ChestSlot;
using ChestSystem.Chests.States.ConcreateStates;
using ChestSystem.Main;
using System.Collections.Generic;

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

            while (queuedChests.Count > 0)
            {
                var nextChest = queuedChests.Dequeue();

                if (nextChest.GetCurrentState() is LockedChestState) // Only start unlocking if chest is still locked
                {
                    currentlyUnlockingChest = nextChest;
                    nextChest.StartUnlocking(unlockedChest);
                    break;
                }
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