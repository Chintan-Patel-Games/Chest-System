using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests.ChestSlot
{
    public class ChestSlotPool
    {
        private List<ChestSlotView> slotList = new();
        private Transform parentTransform;
        private ChestSlotView slotPrefab;

        public ChestSlotPool(ChestSlotView slotPrefab, Transform parentTransform, int initialSize = 5)
        {
            this.slotPrefab = slotPrefab;
            this.parentTransform = parentTransform;

            for (int i = 0; i < 4; i++)
                AddNewSlot();
        }

        private void AddNewSlot()
        {
            ChestSlotView slot = GameObject.Instantiate(slotPrefab, parentTransform);
            slot.SetEmptyState();
            slotList.Add(slot);
        }

        /// <summary>
        /// Finds the first available (empty) slot.
        /// </summary>
        public ChestSlotView GetFirstEmptySlot()
        {
            foreach (var slot in slotList)
            {
                if (slot.IsEmpty()) return slot;
            }

            return null; // No available slot
        }

        public List<ChestSlotView> GetAllSlots() => slotList;
    }
}