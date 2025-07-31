using ChestSystem.Chests.ChestSlot;
using ChestSystem.Main;
using ChestSystem.Utilities;

namespace ChestSystem.Chests.States.ConcreateStates
{
    public class LockedChestState : ChestBaseState
    {
        public LockedChestState(ChestSlotController controller) : base(controller) { }

        public override void EnterState() => controller.SlotLockedState();

        public override void OnChestClicked()
        {
            if (!GameService.Instance.ChestService.CanUnlockChest) // Calling ChestService to check available slots in ChestSlotPool
            {
                GameService.Instance.UIService.ShowMessagePopupUI(StringConstants.UnlockChestWarning);
                return;
            }

            GameService.Instance.UIService.SetTargetSlotForOnClick(controller);
            GameService.Instance.UIService.ShowUnlockChestPopupUI(controller.CalculateUnlockCost());
        }
    }
}