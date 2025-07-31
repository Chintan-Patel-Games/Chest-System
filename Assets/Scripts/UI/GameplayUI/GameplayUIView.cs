using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI.GameplayUI
{
    public class GameplayUIView : MonoBehaviour, IUIView
    {
        [Header("Gameplay UI Elements")]
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private TextMeshProUGUI gemsText;
        [SerializeField] private Button generateChestButton;

        private GameplayUIController controller;

        public void SetController(IUIController controllerToSet)
        {
            controller = controllerToSet as GameplayUIController;
            SubscribeToButtonClicks();
        }

        private void SubscribeToButtonClicks() => generateChestButton.onClick.AddListener(controller.OnGenerateChestClicked);

        public void UpdateCoinsText(int totalCoins) => coinsText.text = totalCoins.ToString();

        public void UpdateGemsText(int totalGems) => gemsText.text = totalGems.ToString();

        public void EnableView() => gameObject.SetActive(true);

        public void DisableView() => gameObject.SetActive(false);
    }
}