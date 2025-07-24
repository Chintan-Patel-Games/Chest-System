using ChestSystem.Utilities;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.Chests
{
    public class ChestSlotView : MonoBehaviour
    {
        [SerializeField] private Button chestSlotButton;
        [SerializeField] private Transform chestViewPlaceholder;
        [SerializeField] private ChestBgImg[] chestBgImages;
        [SerializeField] private TextMeshProUGUI lockedText;
        [SerializeField] private TextMeshProUGUI unlockedText;
        [SerializeField] private TextMeshProUGUI emptyText;
        [SerializeField] private TextMeshProUGUI unlockTimerText;

        private ChestController controller;
        private ChestView chestPrefab;

        //private void Awake() => chestSlotButton.onClick.AddListener(OnSlotClicked);

        public void AssignChest(ChestScriptableObject chestSO, ChestView chestPrefab)
        {
            // Instantiate ChestView inside placeholder
            ChestView chestView = Instantiate(chestPrefab, chestViewPlaceholder);

            controller = new ChestController(chestSO, chestView);

            SetBgImg();

            // Update state to show chest
            SetOccupiedState();
        }

        public bool IsEmpty() => emptyText.gameObject.activeSelf;

        public void SetEmptyState()
        {
            emptyText.gameObject.SetActive(true);
            lockedText.gameObject.SetActive(false);
            unlockedText.gameObject.SetActive(false);
            unlockTimerText.gameObject.SetActive(false);
            chestSlotButton.interactable = false;
        }

        public void SetOccupiedState()
        {
            emptyText.gameObject.SetActive(false);
            lockedText.gameObject.SetActive(true);
            unlockedText.gameObject.SetActive(false);
            SetTimerText();
            unlockTimerText.gameObject.SetActive(true);
            chestSlotButton.interactable = true;
        }

        private void SetTimerText()
        {
            if (controller == null) return;

            var chestData = controller.GetChestData();
            int totalMinutes = (int)chestData.unlockDurationMinutes;
            TimeSpan fullDuration = TimeSpan.FromMinutes(totalMinutes);

            string formattedTime;

            if (totalMinutes <= 30)
            {
                // Show as MM:SS
                formattedTime = StringConstants.GetFormattedRemainingTimeMinutes(fullDuration);
            }
            else
            {
                // Show as HH:MM:SS
                formattedTime = StringConstants.GetFormattedRemainingTimeHours(fullDuration);
            }

            unlockTimerText.text = formattedTime;
        }



        private void SetBgImg()
        {
            foreach (var bgImg in chestBgImages)
            {
                if (bgImg.chestType == controller.GetChestData().chestType)
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
    }

    [System.Serializable]
    public struct ChestBgImg
    {
        public ChestType chestType;
        public Sprite bgImage;
        public Sprite pressedBgImage;
    }
}