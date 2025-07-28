using ChestSystem.Chests.ChestSlot;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI.PopupUI
{
    public class PopupUIView : MonoBehaviour, IUIView
    {
        [SerializeField] private Button unlockWithTimer;
        [SerializeField] private Button unlockWithGems;
        [SerializeField] private Button closePopup;

        private PopupUIController controller;

        public void SetController(IUIController controllerToSet)
        {
            controller = controllerToSet as PopupUIController;
            SubscribeToButtonClicks();
        }

        private void SubscribeToButtonClicks()
        {
            unlockWithTimer.onClick.AddListener(controller.OnUnlockWithTimerClicked);
            unlockWithGems.onClick.AddListener(controller.OnUnlockWithGemsClicked);
            closePopup.onClick.AddListener(DisableView);
        }

        public void EnableView() => gameObject.SetActive(true);

        public void DisableView() => gameObject.SetActive(false);
    }
}