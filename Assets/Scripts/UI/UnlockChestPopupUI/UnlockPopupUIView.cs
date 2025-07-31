using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI.UnlockChestPopupUI
{
    public class UnlockChestPopupUIView : MonoBehaviour, IUIView
    {
        [SerializeField] private Button unlockWithTimer;
        [SerializeField] private Button unlockWithGems;
        [SerializeField] private TextMeshProUGUI gemsText;
        [SerializeField] private Button closePopup;

        private UnlockChestPopupUIController controller;

        public void SetController(IUIController controllerToSet)
        {
            controller = controllerToSet as UnlockChestPopupUIController;
            SubscribeToButtonClicks();
        }

        private void SubscribeToButtonClicks()
        {
            unlockWithTimer.onClick.AddListener(controller.OnUnlockWithTimerClicked);
            unlockWithGems.onClick.AddListener(controller.OnUnlockWithGemsClicked);
            closePopup.onClick.AddListener(controller.OnCloseButtonClicked);
        }

        public void ShowUnlockWithGemsUI() => unlockWithTimer.interactable = false;

        public void SetGemsText(int totalGems) => gemsText.text = totalGems.ToString();

        public void EnableView() => gameObject.SetActive(true);

        public void DisableView() => gameObject.SetActive(false);
    }
}