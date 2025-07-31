using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI.RewardsPopupUI
{
    public class RewardsPopupUIView : MonoBehaviour, IUIView
    {
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private TextMeshProUGUI gemsText;
        [SerializeField] private Button closePopup;

        private RewardsPopupUIController controller;

        public void SetController(IUIController controllerToSet)
        {
            controller = controllerToSet as RewardsPopupUIController;
            SubscribeToButtonClicks();
        }

        public void SetCurrencyText(int totalCoins, int totalGems)
        {
            coinsText.text = totalCoins.ToString();
            gemsText.text = totalGems.ToString();
        }

        private void SubscribeToButtonClicks()
        {
            closePopup.onClick.RemoveAllListeners();
            closePopup.onClick.AddListener(controller.OnCloseButtonClicked);
        }

        public void EnableView() => gameObject.SetActive(true);

        public void DisableView() => gameObject.SetActive(false);
    }
}