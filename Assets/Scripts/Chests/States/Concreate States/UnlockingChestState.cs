using ChestSystem.Chests.ChestSlot;

namespace ChestSystem.Chests.States.ConcreateStates
{
    public class UnlockingChestState : ChestBaseState
    {
        public UnlockingChestState(ChestSlotController controller) : base(controller) { }

        public override void EnterState() => controller.SlotUnlockingState();

        public override void UpdateState()
        {
            //if (controller.HasUnlockTimePassed())
            //{
            //    controller.SetState(new UnlockedChestState(controller));
            //    controller.NotifyChestReady();
            //}
        }

        public override void OnChestClicked()
        {
            // Optionally show timer progress or locked message
        }
    }
}