using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI
{
    public class GameplayUIView : MonoBehaviour, IUIView
    {
        [SerializeField] private Button generateChestButton;

        private GameplayUIController controller;

        public void SetController(IUIController controllerToSet)
        {
            controller = controllerToSet as GameplayUIController;
            SubscribeToButtonClicks();
        }

        private void SubscribeToButtonClicks() => generateChestButton.onClick.AddListener(controller.OnGenerateChestClicked);

        public void DisableView() => gameObject.SetActive(false);

        public void EnableView() => gameObject.SetActive(true);
    }
}