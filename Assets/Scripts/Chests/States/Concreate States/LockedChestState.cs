using ChestSystem.Chests.ChestSlot;
using ChestSystem.Main;

namespace ChestSystem.Chests.States.ConcreateStates
{
    public class LockedChestState : ChestBaseState
    {
        public LockedChestState(ChestSlotController controller) : base(controller) { }

        public override void EnterState() => controller.SlotLockedState();

        public override void OnChestClicked()
        {
            GameService.Instance.UIService.SetTargetSlot(controller);
            GameService.Instance.UIService.ShowPopupUI();
        }
    }
}