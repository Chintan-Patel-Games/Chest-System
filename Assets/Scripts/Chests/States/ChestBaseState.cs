using ChestSystem.Chests.ChestSlot;

namespace ChestSystem.Chests.States
{
    public abstract class ChestBaseState
    {
        protected ChestSlotController controller;

        public ChestBaseState(ChestSlotController controller) => this.controller = controller;

        public virtual void EnterState() { }
        public virtual void OnSlotClicked() { }
        public virtual void UpdateState() { }
    }
}