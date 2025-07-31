using ChestSystem.Chests.ChestSlot;
using ChestSystem.UI.ChestSlotPoolUI;
using ChestSystem.UI.CollectChestPopupUI;
using ChestSystem.UI.ExitGamePopupUI;
using ChestSystem.UI.GameplayUI;
using ChestSystem.UI.MessagePopupUI;
using ChestSystem.UI.RewardsPopupUI;
using ChestSystem.UI.UnlockChestPopupUI;
using ChestSystem.UI.UnlockWithGemsPopupUI;
using UnityEngine;

namespace ChestSystem.UI
{
    public class UIService : MonoBehaviour
    {
        [Tooltip("ChestSlotPoolUI")]
        [SerializeField] private ChestSlotPoolUIView chestSlotPoolView;
        private ChestSlotPoolUIController chestSlotPoolController;

        [Tooltip("GameplayUI")]
        [SerializeField] private GameplayUIView gameplayView;
        private GameplayUIController gameplayController;

        [Tooltip("UnlockChestPopupUI")]
        [SerializeField] private UnlockChestPopupUIView unlockChestPopupView;
        private UnlockChestPopupUIController unlockChestPopupController;

        [Tooltip("UnlockWithPopupUI")]
        [SerializeField] private UnlockwithGemsPopupUIView unlockwithGemsPopupUIView;
        private UnlockWithGemsPopupUIController unlockWithGemsPopupUIController;

        [Tooltip("CollectChestPopupUI")]
        [SerializeField] private CollectChestPopupUIView collectChestPopupView;
        private CollectChestPopupUIController collectChestPopupController;

        [Tooltip("RewardsPopupUI")]
        [SerializeField] private RewardsPopupUIView rewardsPopupView;
        private RewardsPopupUIController rewardsPopupController;

        [Tooltip("MessagePopupUI")]
        [SerializeField] private MessagePopupUIView messagePopUIView;
        private MessagePopupUIController messagePopupUIController;

        [Tooltip("ExitGamePopupUI")]
        [SerializeField] private ExitGamePopupUIView exitGamePopupView;
        private ExitGamePopupUIController exitGamePopupUIController;

        [Header("Raycast Control")]
        [SerializeField] private CanvasGroup gameplayUICanvasGroup;
        [SerializeField] private CanvasGroup chestSlotPoolCanvasGroup;

        private void Start()
        {
            chestSlotPoolController = new ChestSlotPoolUIController(chestSlotPoolView);
            gameplayController = new GameplayUIController(gameplayView);
            unlockChestPopupController = new UnlockChestPopupUIController(unlockChestPopupView);
            unlockWithGemsPopupUIController = new UnlockWithGemsPopupUIController(unlockwithGemsPopupUIView);
            collectChestPopupController = new CollectChestPopupUIController(collectChestPopupView);
            rewardsPopupController = new RewardsPopupUIController(rewardsPopupView);
            messagePopupUIController = new MessagePopupUIController(messagePopUIView);
            exitGamePopupUIController = new ExitGamePopupUIController(exitGamePopupView);
        }

        public ChestSlotView AddNewSlot() => chestSlotPoolController.AddNewSlot();

        public void SetTargetSlotForUnlock(ChestSlotController controller) => unlockChestPopupController.SetTargetSlot(controller);

        public void SetTargetSlotForUnlockWithGems(ChestSlotController controller) => unlockWithGemsPopupUIController.SetTargetSlot(controller);

        public void SetTargetSlotForRewards(ChestSlotController controller) => rewardsPopupController.SetTargetSlot(controller);

        public void SetTargetSlotForCollect(ChestSlotController controller) => collectChestPopupController.SetTargetSlot(controller);

        public void UpdateCoinsText(int totalCoins) => gameplayController.UpdateCoinsText(totalCoins);

        public void UpdateGemsText(int totalGems) => gameplayController.UpdateGemsText(totalGems);

        public void ShowUnlockChestPopupUI(int unlockCost)
        {
            SetUIRaycastBlock(false);
            unlockChestPopupController.Show(unlockCost);
        }

        public void ShowUnlockWithGemsUI(int totalGems)
        {
            SetUIRaycastBlock(false);
            unlockWithGemsPopupUIController.Show(totalGems);
        }

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

        public void ShowWarningPopupUI(string message)
        {
            SetUIRaycastBlock(false);
            messagePopupUIController.ShowWarningPopup(message);
        }

        public void ShowMessagePopupUI(string message)
        {
            SetUIRaycastBlock(false);
            messagePopupUIController.ShowMessagePopup(message);
        }

        public void ShowExitPopupUI() => exitGamePopupUIController.Show();

        public void SetUIRaycastBlock(bool enable)
        {
            gameplayUICanvasGroup.blocksRaycasts = enable;
            chestSlotPoolCanvasGroup.blocksRaycasts = enable;
        }
    }
}