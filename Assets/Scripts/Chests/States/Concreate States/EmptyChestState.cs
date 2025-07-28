using ChestSystem.Chests.ChestSlot;
using ChestSystem.Chests.States;

public class EmptyChestState : ChestBaseState
{
    public EmptyChestState(ChestSlotController controller) : base(controller) { }

    public override void EnterState() => controller.SlotEmptyState();
}