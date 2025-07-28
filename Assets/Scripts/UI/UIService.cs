using ChestSystem.Chests.ChestSlot;
using ChestSystem.UI.ChestSlotPoolUI;
using ChestSystem.UI.GameplayUI;
using ChestSystem.UI.PopupUI;
using ChestSystem.UI.RewardsPopupUI;
using UnityEngine;

namespace ChestSystem.UI
{
    public class UIService : MonoBehaviour
    {
        [Header("ChestSlotPoolUI")]
        private ChestSlotPoolUIController chestSlotPoolController;
        [SerializeField] private ChestSlotPoolUIView chestSlotPoolView;

        [Header("GameplayUI")]
        private GameplayUIController gameplayController;
        [SerializeField] private GameplayUIView gameplayView;

        [Header("PopupUI")]
        private PopupUIController popupController;
        [SerializeField] private PopupUIView popupView;

        [Header("RewardsPopupUI")]
        private RewardsPopupUIController rewardsPopupController;
        [SerializeField] private RewardsPopupUIView rewardsPopupView;

        private void Start()
        {
            chestSlotPoolController = new ChestSlotPoolUIController(chestSlotPoolView);
            gameplayController = new GameplayUIController(gameplayView);
            popupController = new PopupUIController(popupView);
            rewardsPopupController = new RewardsPopupUIController(rewardsPopupView);
        }

        public ChestSlotView AddNewSlot() => chestSlotPoolController.AddNewSlot();

        public void SetTargetSlot(ChestSlotController controller) => popupController.SetTargetSlot(controller);

        public void ShowPopupUI() => popupController.Show();

        public void ShowRewardsPopupUI() => rewardsPopupController.Show();
    }
}