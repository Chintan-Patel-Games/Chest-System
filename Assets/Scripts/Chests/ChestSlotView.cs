using TMPro;
using UnityEngine;

namespace ChestSystem.Chests
{
    public class ChestSlotView : MonoBehaviour
    {
        [SerializeField] private ChestView chestView;
        [SerializeField] private TextMeshProUGUI lockedText;
        [SerializeField] private TextMeshProUGUI unlockedText;
        [SerializeField] private TextMeshProUGUI emptyText;
        [SerializeField] private TextMeshProUGUI unlockTimerText;

        private ChestScriptableObject currentChest;
        private ChestView chestViewInstance;

        public bool IsEmpty() => currentChest == null;

        public void AssignChest(ChestScriptableObject chestSO)
        {
            currentChest = chestSO;

            emptyText.gameObject.SetActive(false);
            lockedText.gameObject.SetActive(true);
            unlockedText.gameObject.SetActive(false);

            chestView.gameObject.SetActive(true);
            chestView.SetVisuals(chestSO.chestSprite, chestSO.animatorController);
        }
    }
}