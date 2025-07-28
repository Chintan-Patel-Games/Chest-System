using ChestSystem.Utilities;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.Chests.ChestSlot
{
    public class ChestSlotView : MonoBehaviour
    {
        [SerializeField] private ChestView chestPrefab;
        [SerializeField] private Button chestSlotButton;
        [SerializeField] private Transform chestViewPlaceholder;
        [SerializeField] private ChestBgImg[] chestBgImages;
        [SerializeField] private TextMeshProUGUI emptyText;
        [SerializeField] private TextMeshProUGUI lockedText;
        [SerializeField] private TextMeshProUGUI unlockingText;
        [SerializeField] private TextMeshProUGUI unlockedText;
        [SerializeField] private TextMeshProUGUI unlockTimerText;

        private ChestController chestController;

        public void SetController(ChestSlotController controller) => chestSlotButton.onClick.AddListener(() => controller.OnSlotClicked());

        public ChestSlotView CreateChest(ChestSO chestSO)
        {
            chestController = new ChestController(chestSO, Instantiate(chestPrefab, chestViewPlaceholder));
            SetBgImg();
            return this;
        }

        public void SlotEmptyState()
        {
            emptyText.gameObject.SetActive(true);
            lockedText.gameObject.SetActive(false);
            unlockingText.gameObject.SetActive(false);
            unlockedText.gameObject.SetActive(false);
            unlockTimerText.gameObject.SetActive(false);
            chestSlotButton.interactable = false;
        }

        public void SlotLockedState()
        {
            emptyText.gameObject.SetActive(false);
            lockedText.gameObject.SetActive(true);
            unlockingText.gameObject.SetActive(false);
            unlockedText.gameObject.SetActive(false);
            SetTimerText();
            unlockTimerText.gameObject.SetActive(true);
            chestSlotButton.interactable = true;
        }

        public void SlotUnlockingState()
        {
            emptyText.gameObject.SetActive(false);
            lockedText.gameObject.SetActive(false);
            unlockingText.gameObject.SetActive(true);
            unlockedText.gameObject.SetActive(false);
            unlockTimerText.gameObject.SetActive(true);
        }

        public void SlotUnlockedState()
        {
            emptyText.gameObject.SetActive(false);
            lockedText.gameObject.SetActive(false);
            unlockingText.gameObject.SetActive(false);
            unlockedText.gameObject.SetActive(true);
            unlockTimerText.gameObject.SetActive(true);
        }

        public bool IsEmpty() => emptyText.gameObject.activeSelf;

        public void SetTimerText()
        {
            if (chestController == null) return;

            var chestData = chestController.GetChestData();
            int totalMinutes = (int)chestData.unlockDurationMinutes;
            TimeSpan fullDuration = TimeSpan.FromMinutes(totalMinutes);
            string formattedTime = StringConstants.FormatRemainingTime(fullDuration);
            unlockTimerText.text = formattedTime;
        }

        public void SetBgImg()
        {
            foreach (var bgImg in chestBgImages)
            {
                if (bgImg.chestType == chestController.GetChestData().chestType)
                {
                    // Set background image
                    gameObject.GetComponent<Image>().sprite = bgImg.bgImage;

                    // Set the button's pressed sprite properly
                    SpriteState spriteState = chestSlotButton.spriteState;
                    spriteState.pressedSprite = bgImg.pressedBgImage;
                    chestSlotButton.spriteState = spriteState;

                    return;
                }
            }
        }

        public ChestController GetChestController() => chestController;

        public void OnDestroy() => chestController = null;
    }

    [System.Serializable]
    public struct ChestBgImg
    {
        public ChestType chestType;
        public Sprite bgImage;
        public Sprite pressedBgImage;
    }
}