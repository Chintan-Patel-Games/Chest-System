using ChestSystem.Main;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chests.ChestSlot
{
    public class ChestSlotPool
    {
        private List<ChestSlotController> slotList;
        private int maxSlots = 4; // Maximum number of slots

        public ChestSlotPool()
        {
            slotList = new List<ChestSlotController>();

            for (int i = 0; i < maxSlots; i++)
            {
                ChestSlotView view = GameService.Instance.UIService.AddNewSlot();
                ChestSlotController controller = new ChestSlotController(view);
                view.SetController(controller);
                slotList.Add(controller);
            }
        }

        public void AssignChest(List<ChestSO> chestList)
        {
            ChestSO randomChest = GetRandomChest(chestList);

            ChestSlotController emptySlot = GetFirstEmptySlot();
            if (emptySlot != null)
                emptySlot.AssignChest(randomChest);
        }

        private ChestSlotController GetFirstEmptySlot()
        {
            foreach (var slot in slotList)
                if (slot.IsEmpty()) return slot;

            return null; // No available slot
        }

        private ChestSO GetRandomChest(List<ChestSO> chests) => chests[Random.Range(0, chests.Count)];
    }
}