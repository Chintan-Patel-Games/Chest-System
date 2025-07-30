using ChestSystem.Utilities;
using System;
using System.Collections;
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
        [SerializeField] private TextMeshProUGUI chestTypeText;

        private ChestController chestController;
        private ChestView chestView;

        private Coroutine unlockTimerCoroutine;

        public void SetController(ChestSlotController controller)
        {
            chestSlotButton.onClick.RemoveAllListeners();
            chestSlotButton.onClick.AddListener(() => controller.OnSlotClicked());
        }

        public ChestSlotView CreateChest(ChestSO chestSO)
        {
            chestView = Instantiate(chestPrefab, chestViewPlaceholder);
            chestController = new ChestController(chestSO, chestView);
            SetBgImg();
            return this;
        }

        public void RemoveChestView()
        {
            if (chestView != null)
            {
                Destroy(chestView.gameObject);
                chestController = null;
                chestView = null;
            }
        }

        public void SlotEmptyState()
        {
            emptyText.gameObject.SetActive(true);
            lockedText.gameObject.SetActive(false);
            unlockingText.gameObject.SetActive(false);
            unlockedText.gameObject.SetActive(false);
            unlockTimerText.gameObject.SetActive(false);
            chestTypeText.gameObject.SetActive(false);
            chestSlotButton.interactable = false;
        }

        public void SlotLockedState()
        {
            emptyText.gameObject.SetActive(false);
            lockedText.gameObject.SetActive(true);
            unlockedText.gameObject.SetActive(false);
            SetTimerText();
            unlockTimerText.gameObject.SetActive(true);
            SetChestTypeText();
            chestTypeText.gameObject.SetActive(true);
            chestSlotButton.interactable = true;
        }

        public void SlotUnlockingState()
        {
            lockedText.gameObject.SetActive(false);
            unlockingText.gameObject.SetActive(true);
        }

        public void SlotUnlockedState()
        {
            lockedText.gameObject.SetActive(false);
            unlockingText.gameObject.SetActive(false);
            unlockedText.gameObject.SetActive(true);
            unlockTimerText.gameObject.SetActive(false);
            chestTypeText.gameObject.SetActive(true);
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

        public void SetChestTypeText()
        {
            if (chestController == null) return;
            var chestData = chestController.GetChestData();

            switch (chestData.chestType)
            {
                case ChestType.COMMON:
                    chestTypeText.text ="Common";
                    return;
                case ChestType.EPIC:
                    chestTypeText.text = "Epic";
                    return;
                case ChestType.RARE:
                    chestTypeText.text = "Rare";
                    return;
                case ChestType.LEGENDARY:
                    chestTypeText.text = "Legendary";
                    return;
                default:
                    chestTypeText.text = "Unknown";
                    return;
            }
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

        public void StartUnlockTimer(Func<TimeSpan> getRemainingTime, Action onFinished)
        {
            if (unlockTimerCoroutine != null)
                StopCoroutine(unlockTimerCoroutine);

            unlockTimerCoroutine = StartCoroutine(RunUnlockTimer(getRemainingTime, onFinished));
        }

        private IEnumerator RunUnlockTimer(Func<TimeSpan> getRemainingTime, Action onFinished)
        {
            while (true)
            {
                TimeSpan remaining = getRemainingTime();
                unlockTimerText.text = StringConstants.FormatRemainingTime(remaining);

                if (remaining.TotalSeconds <= 0)
                {
                    onFinished?.Invoke();
                    yield break;
                }

                yield return new WaitForSeconds(1f); // Update every second
            }
        }

        public void StopUnlockTimer()
        {
            if (unlockTimerCoroutine != null)
                StopCoroutine(unlockTimerCoroutine);
            unlockTimerCoroutine = null;
        }

        public void ResetTimer()
        {
            StopUnlockTimer();
            SetTimerText();
        }

        public ChestController GetChestController() => chestController;

        public void OnDestroy()
        {
            chestSlotButton.onClick.RemoveAllListeners();
            StopUnlockTimer();
            chestController = null;
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