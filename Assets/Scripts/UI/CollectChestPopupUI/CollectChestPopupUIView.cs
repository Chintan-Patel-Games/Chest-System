using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI.CollectChestPopupUI
{
    public class CollectChestPopupUIView : MonoBehaviour, IUIView
    {
        [SerializeField] private Button undoUnlock;
        [SerializeField] private Button collectRewards;
        [SerializeField] private Button closePopup;

        private CollectChestPopupUIController controller;

        public void SetController(IUIController controllerToSet)
        {
            controller = controllerToSet as CollectChestPopupUIController;
            SubscribeToButtonClicks();
        }

        private void SubscribeToButtonClicks()
        {
            undoUnlock.onClick.AddListener(controller.OnUndoUnlockClicked);
            collectRewards.onClick.AddListener(controller.OnCollectRewardsClicked);
            closePopup.onClick.AddListener(controller.OnCloseButtonClicked);
        }

        public void EnableView() => gameObject.SetActive(true);

        public void DisableView() => gameObject.SetActive(false);
    }
}