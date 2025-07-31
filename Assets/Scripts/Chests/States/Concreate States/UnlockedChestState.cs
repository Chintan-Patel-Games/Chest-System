using ChestSystem.Chests.ChestSlot;
using ChestSystem.Main;

namespace ChestSystem.Chests.States.ConcreateStates
{
    public class UnlockedChestState : ChestBaseState
    {
        public UnlockedChestState(ChestSlotController controller) : base(controller) { }

        public override void EnterState() => controller.SlotUnlockedState();

        public override void OnSlotClicked()
        {
            GameService.Instance.UIService.SetTargetSlotForCollect(controller);
            GameService.Instance.UIService.ShowCollectChestPopupUI();
        }
    }
}