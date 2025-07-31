using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI.ExitGamePopupUI
{
    public class ExitGamePopupUIView : MonoBehaviour, IUIView
    {
        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;

        private ExitGamePopupUIController controller;

        public void SetController(IUIController controllerToSet)
        {
            controller = controllerToSet as ExitGamePopupUIController;
            SubscribeToButtonClicks();
        }

        private void SubscribeToButtonClicks()
        {
            yesButton.onClick.AddListener(controller.OnYesButtonClicked);
            noButton.onClick.AddListener(controller.OnExitGameClicked);
        }

        public void EnableView() => gameObject.SetActive(true);

        public void DisableView() => gameObject.SetActive(false);
    }
}