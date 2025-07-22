namespace ChestSystem.Chests.States
{
    public abstract class ChestBaseState
    {
        protected ChestController controller;

        public ChestBaseState(ChestController controller) => this.controller = controller;

        public virtual void EnterState() { }
        public virtual void OnChestClicked() { }
        public virtual void UpdateState() { }
    }
}