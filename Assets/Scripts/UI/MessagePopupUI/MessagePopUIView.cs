using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI.MessagePopupUI
{
    public class MessagePopupUIView : MonoBehaviour, IUIView
    {
        [SerializeField] private GameObject warningTxt;
        [SerializeField] private GameObject messageTxt;
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

        public void EnableWarningView()
        {
            warningTxt.SetActive(true);
            messageTxt.SetActive(false);
            EnableView();
        }

        public void EnableMessageView()
        {
            warningTxt.SetActive(false);
            messageTxt.SetActive(true);
            EnableView();
        }

        public void EnableView() => gameObject.SetActive(true);

        public void DisableView() => gameObject.SetActive(false);
    }
}