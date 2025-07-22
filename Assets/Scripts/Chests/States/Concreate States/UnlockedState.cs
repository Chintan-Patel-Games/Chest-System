namespace ChestSystem.Chests.States.ConcreateStates
{
    public class UnlockedChestState : ChestBaseState
    {
        public UnlockedChestState(ChestController controller) : base(controller) { }

        public override void OnChestClicked() => controller.CollectChest();
    }
}