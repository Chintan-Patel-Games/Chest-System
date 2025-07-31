using ChestSystem.Chests.ChestSlot;
using UnityEngine;

namespace ChestSystem.UI.ChestSlotPoolUI
{
    public class ChestSlotPoolUIView : MonoBehaviour, IUIView
    {
        [SerializeField] private ChestSlotView chestSlotPrefab;
        [SerializeField] private Transform chestSlotPlaceholder;

        private ChestSlotPoolUIController controller;

        public void SetController(IUIController controllerToSet) => controller = controllerToSet as ChestSlotPoolUIController;

        public ChestSlotView AddNewSlot() => Instantiate(chestSlotPrefab, chestSlotPlaceholder);

        public void EnableView() => gameObject.SetActive(true);

        public void DisableView() => gameObject.SetActive(false);
    }
}