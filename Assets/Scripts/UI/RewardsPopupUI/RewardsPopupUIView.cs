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

        public void SetCoinsText(int coins) => coinsText.text = coins.ToString();

        public void SetGemsText(int gems) => gemsText.text = gems.ToString();

        private void SubscribeToButtonClicks() => closePopup.onClick.AddListener(DisableView);

        public void EnableView() => gameObject.SetActive(true);

        public void DisableView() => gameObject.SetActive(false);
    }
}