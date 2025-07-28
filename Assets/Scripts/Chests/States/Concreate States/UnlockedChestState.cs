using ChestSystem.Chests.ChestSlot;

namespace ChestSystem.Chests.States.ConcreateStates
{
    public class UnlockedChestState : ChestBaseState
    {
        public UnlockedChestState(ChestSlotController controller) : base(controller) { }

        public override void EnterState() => controller.SlotUnlockedState();

        public override void OnChestClicked() => controller.CollectChest();
    }
}