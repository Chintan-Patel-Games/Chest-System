using ChestSystem.Chests.ChestSlot;

namespace ChestSystem.Events
{
    public class EventService
    {
        public EventController OnGenerateChest { get; private set; }
        public EventController<ChestSlotController> OnUnlockWithTimer { get; private set; }
        public EventController<ChestSlotController> OnUnlockWithGems { get; private set; }
        public EventController OnChestReadyToOpen { get; private set; }
        public EventController<ChestSlotController> OnCollectChest { get; private set; }
        public EventController<ChestSlotController> OnUndo { get; private set; }
        public EventController<ChestSlotController> OnChestRemove { get; private set; }

        public EventService()
        {
            OnGenerateChest = new EventController();
            OnUnlockWithTimer = new EventController<ChestSlotController>();
            OnUnlockWithGems = new EventController<ChestSlotController>();
            OnChestReadyToOpen = new EventController();
            OnCollectChest = new EventController<ChestSlotController>();
            OnUndo = new EventController<ChestSlotController>();
            OnChestRemove = new EventController<ChestSlotController>();
        }
    }
}