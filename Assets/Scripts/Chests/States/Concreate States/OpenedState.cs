using ChestSystem.Chests;
using ChestSystem.Chests.States;

public class OpenedChestState : ChestBaseState
{
    public OpenedChestState(ChestController controller) : base(controller) { }

    //public override void EnterState() => controller.OnChestOpened.InvokeEvent(null);

    public override void OnChestClicked()
    {
        // Chest already opened – maybe show rewards again or hide UI
    }
}