using ChestSystem.Chests.ChestSlot;
using ChestSystem.UI.ChestSlotPoolUI;
using ChestSystem.UI.CollectChestPopupUI;
using ChestSystem.UI.GameplayUI;
using ChestSystem.UI.MessagePopupUI;
using ChestSystem.UI.RewardsPopupUI;
using ChestSystem.UI.UnlockChestPopupUI;
using UnityEngine;

namespace ChestSystem.UI
{
    public class UIService : MonoBehaviour
    {
        [Tooltip("ChestSlotPoolUI")]
        private ChestSlotPoolUIController chestSlotPoolController;
        [SerializeField] private ChestSlotPoolUIView chestSlotPoolView;

        [Tooltip("GameplayUI")]
        private GameplayUIController gameplayController;
        [SerializeField] private GameplayUIView gameplayView;

        [Tooltip("UnlockChestPopupUI")]
        private UnlockChestPopupUIController unlockChestPopupController;
        [SerializeField] private UnlockChestPopupUIView unlockChestPopupView;

        [Tooltip("CollectChestPopupUI")]
        private CollectChestPopupUIController collectChestPopupController;
        [SerializeField] private CollectChestPopupUIView collectChestPopupView;

        [Tooltip("RewardsPopupUI")]
        private RewardsPopupUIController rewardsPopupController;
        [SerializeField] private RewardsPopupUIView rewardsPopupView;

        [Tooltip("MessagePopupUI")]
        private MessagePopupUIController messagePopupUIController;
        [SerializeField] private MessagePopupUIView messagePopUIView;

        [Header("Raycast Control")]
        [SerializeField] private CanvasGroup gameplayUICanvasGroup;
        [SerializeField] private CanvasGroup chestSlotPoolCanvasGroup;

        private void Start()
        {
            chestSlotPoolController = new ChestSlotPoolUIController(chestSlotPoolView);
            gameplayController = new GameplayUIController(gameplayView);
            unlockChestPopupController = new UnlockChestPopupUIController(unlockChestPopupView);
            collectChestPopupController = new CollectChestPopupUIController(collectChestPopupView);
            rewardsPopupController = new RewardsPopupUIController(rewardsPopupView);
            messagePopupUIController = new MessagePopupUIController(messagePopUIView);
        }

        public ChestSlotView AddNewSlot() => chestSlotPoolController.AddNewSlot();

        public void SetTargetSlotForOnClick(ChestSlotController controller) => unlockChestPopupController.SetTargetSlot(controller);

        public void SetTargetSlotForRemove(ChestSlotController controller) => rewardsPopupController.SetTargetSlot(controller);

        public void SetTargetSlotForCollect(ChestSlotController controller) => collectChestPopupController.SetTargetSlot(controller);

        public void UpdateCoinsText(int totalCoins) => gameplayController.UpdateCoinsText(totalCoins);

        public void UpdateGemsText(int totalGems) => gameplayController.UpdateGemsText(totalGems);

        public void ShowUnlockChestPopupUI(int unlockCost)
        {
            SetUIRaycastBlock(false);
            unlockChestPopupController.Show(unlockCost);
        }

        public void ShowUnlockWithGemsUI(int totalGems) => unlockChestPopupController.ShowUnlockWithChestUI(totalGems);

        public void ShowCollectChestPopupUI()
        {
            SetUIRaycastBlock(false);
            collectChestPopupController.Show();
        }

        public void ShowRewardsPopupUI(int totalCoins, int totalGems)
        {
            SetUIRaycastBlock(false);
            rewardsPopupController.SetRewards(totalCoins, totalGems);
            rewardsPopupController.Show();
        }

        public void ShowMessagePopupUI(string message)
        {
            SetUIRaycastBlock(false);
            messagePopupUIController.SetMessageText(message);
        }

        public void SetUIRaycastBlock(bool enable)
        {
            gameplayUICanvasGroup.blocksRaycasts = enable;
            chestSlotPoolCanvasGroup.blocksRaycasts = enable;
        }
    }
}