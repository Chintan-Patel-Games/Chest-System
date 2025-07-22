namespace ChestSystem.Chests.States.ConcreateStates
{
    public class UnlockingChestState : ChestBaseState
    {
        public UnlockingChestState(ChestController controller) : base(controller) { }

        public override void UpdateState()
        {
            if (controller.HasUnlockTimePassed())
                controller.SetState(new UnlockedChestState(controller));
        }
    }
}