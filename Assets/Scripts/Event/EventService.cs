using ChestSystem.Chests.States;

namespace ChestSystem.Events
{
    public class EventService
    {
        public EventController<object> OnGenerateChest { get; private set; }
        public EventController<ChestState> OnChestStateChanged { get; private set; }
        public EventController<object> OnChestReadyToOpen { get; private set; }
        public EventController<object> OnChestOpened { get; private set; }

        public EventService()
        {
            OnGenerateChest = new EventController<object>();
            OnChestStateChanged = new EventController<ChestState>();
            OnChestReadyToOpen = new EventController<object>();
            OnChestOpened = new EventController<object>();
        }
    }
}