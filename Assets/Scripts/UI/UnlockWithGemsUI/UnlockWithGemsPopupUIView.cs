using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI.UnlockWithGemsPopupUI
{
    public class UnlockwithGemsPopupUIView : MonoBehaviour, IUIView
    {
        [SerializeField] private Button unlockWithGems;
        [SerializeField] private TextMeshProUGUI gemsText;
        [SerializeField] private Button closePopup;

        private UnlockWithGemsPopupUIController controller;

        public void SetController(IUIController controllerToSet)
        {
            controller = controllerToSet as UnlockWithGemsPopupUIController;
            SubscribeToButtonClicks();
        }

        private void SubscribeToButtonClicks()
        {
            unlockWithGems.onClick.AddListener(controller.OnUnlockWithGemsClicked);
            closePopup.onClick.AddListener(controller.OnCloseButtonClicked);
        }

        public void SetGemsText(int totalGems) => gemsText.text = totalGems.ToString();

        public void EnableView() => gameObject.SetActive(true);

        public void DisableView() => gameObject.SetActive(false);
    }
}