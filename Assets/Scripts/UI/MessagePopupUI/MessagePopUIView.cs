using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI.MessagePopupUI
{
    public class MessagePopupUIView : MonoBehaviour, IUIView
    {
        [SerializeField] private TextMeshProUGUI message;
        [SerializeField] private Button closePopup;

        private MessagePopupUIController controller;

        public void SetController(IUIController controllerToSet)
        {
            controller = controllerToSet as MessagePopupUIController;
            SubscribeToButtonClicks();
        }

        private void SubscribeToButtonClicks() => closePopup.onClick.AddListener(controller.OnClosePopup);

        public void SetMessageText(string message) => this.message.text = message;

        public void EnableView() => gameObject.SetActive(true);

        public void DisableView() => gameObject.SetActive(false);
    }
}