using ChestSystem.Chests.ChestSlot;
using ChestSystem.Main;

namespace ChestSystem.Chests.States.ConcreateStates
{
    public class UnlockingChestState : ChestBaseState
    {
        public UnlockingChestState(ChestSlotController controller) : base(controller) { }

        public override void EnterState() => controller.SlotUnlockingState();

        public override void OnSlotClicked()
        {
            GameService.Instance.UIService.SetTargetSlotForUnlockWithGems(controller);
            GameService.Instance.UIService.ShowUnlockWithGemsUI(controller.CalculateUnlockCost());
        }

        public override void UpdateState()
        {
            if (controller.HasUnlockTimePassed())
            {
                controller.SetState(new UnlockedChestState(controller));
                GameService.Instance.EventService.OnChestReadyToOpen.InvokeEvent(controller);
            }
        }
    }
}