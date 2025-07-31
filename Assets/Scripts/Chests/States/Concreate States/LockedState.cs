namespace ChestSystem.Chests.States.ConcreateStates
{
    public class LockedChestState : ChestBaseState
    {
        public LockedChestState(ChestController controller) : base(controller) { }

        public override void OnChestClicked() => controller.StartUnlocking();
    }
}