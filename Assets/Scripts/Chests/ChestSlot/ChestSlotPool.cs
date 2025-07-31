using ChestSystem.Chests.States.ConcreateStates;
using ChestSystem.Main;
using ChestSystem.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests.ChestSlot
{
    public class ChestSlotPool
    {
        private List<ChestSlotController> slotList;
        private ChestSlotController controller;
        private int maxSlots = 4; // Maximum number of slots

        public ChestSlotPool()
        {
            slotList = new List<ChestSlotController>();

            for (int i = 0; i < maxSlots; i++)
            {
                controller = new ChestSlotController();
                slotList.Add(controller);
            }
        }

        public void AssignChest(List<ChestSO> chestList)
        {
            ChestSO randomChest = GetRandomChest(chestList);

            ChestSlotController emptySlot = GetFirstEmptySlot();
            if (emptySlot != null)
                emptySlot.AssignChest(randomChest);
            else
                GameService.Instance.UIService.ShowWarningPopupUI(StringConstants.NoEmptySlotsAvailable);
        }

        public void UpdateChestSlots()
        {
            foreach (var slot in slotList)
                slot.UpdateState();
        }

        public bool CanUnlockChest()
        {
            foreach (var slot in slotList)
                if (slot.GetCurrentState() is UnlockingChestState) return false;
            return true;
        }

        private ChestSlotController GetFirstEmptySlot()
        {
            foreach (var slot in slotList)
                if (slot.IsSlotEmpty()) return slot;

            return null; // No available slot
        }

        private ChestSO GetRandomChest(List<ChestSO> chests) => chests[Random.Range(0, chests.Count)];

        ~ChestSlotPool() => slotList.Clear();
    }
}