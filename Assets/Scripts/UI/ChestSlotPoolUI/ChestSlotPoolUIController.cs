using ChestSystem.Chests.ChestSlot;

namespace ChestSystem.UI.ChestSlotPoolUI
{
    public class ChestSlotPoolUIController : IUIController
    {
        private ChestSlotPoolUIView view;

        public ChestSlotPoolUIController(ChestSlotPoolUIView view)
        {
            this.view = view;
            view.SetController(this);
        }

        public ChestSlotView AddNewSlot() => view.AddNewSlot();
    }
}