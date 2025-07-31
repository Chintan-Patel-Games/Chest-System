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
        [SerializeField] private ChestView chestView;
        [SerializeField] private Transform chestViewPlaceholder;
        [SerializeField] private GameObject openChestPlaceholder;
        [SerializeField] private Button chestSlotButton;
        [SerializeField] private ChestBgImg[] chestBgImages;
        [SerializeField] private TextMeshProUGUI emptyText;
        [SerializeField] private TextMeshProUGUI lockedText;
        [SerializeField] private TextMeshProUGUI unlockingText;
        [SerializeField] private TextMeshProUGUI gemText;
        [SerializeField] private TextMeshProUGUI unlockedText;
        [SerializeField] private TextMeshProUGUI unlockTimerText;
        [SerializeField] private TextMeshProUGUI chestTypeText;

        private Coroutine unlockTimerCoroutine;

        private void Awake()
        {
            chestView = chestViewPlaceholder.GetComponentInChildren<ChestView>();
            chestView.gameObject.SetActive(false);
            openChestPlaceholder.SetActive(false);
        }

        public void SubscribeToButtons(ChestSlotController controller)
        {
            chestSlotButton.onClick.RemoveAllListeners();
            chestSlotButton.onClick.AddListener(() => controller.OnSlotClicked());
        }

        public ChestSlotView ActivateChestView(ChestType chestType)
        {
            chestView.gameObject.SetActive(true);
            SetBgImg(chestType);
            return this;
        }

        public void DeactivateChestView() => chestView.gameObject.SetActive(false);

        public void SetEmptyStateView()
        {
            emptyText.gameObject.SetActive(true);
            lockedText.gameObject.SetActive(false);
            unlockingText.gameObject.SetActive(false);
            unlockedText.gameObject.SetActive(false);
            unlockTimerText.gameObject.SetActive(false);
            chestTypeText.gameObject.SetActive(false);
            chestSlotButton.interactable = false;
        }

        public void SetLockedStateView(int totalMinutes, string chestTypeText)
        {
            emptyText.gameObject.SetActive(false);
            lockedText.gameObject.SetActive(true);
            unlockedText.gameObject.SetActive(false);
            SetTimerText(totalMinutes);
            unlockTimerText.gameObject.SetActive(true);
            this.chestTypeText.text = chestTypeText;
            this.chestTypeText.gameObject.SetActive(true);
            chestSlotButton.interactable = true;
        }

        public void SetUnlockingStateView()
        {
            lockedText.gameObject.SetActive(false);
            unlockingText.gameObject.SetActive(true);
            openChestPlaceholder.SetActive(true);
        }

        public void SetUnlockedStateView()
        {
            lockedText.gameObject.SetActive(false);
            unlockingText.gameObject.SetActive(false);
            unlockedText.gameObject.SetActive(true);
            unlockTimerText.gameObject.SetActive(false);
            chestTypeText.gameObject.SetActive(true);
            openChestPlaceholder.SetActive(false);
        }

        public void SetTimerText(int totalMinutes)
        {
            TimeSpan fullDuration = TimeSpan.FromMinutes(totalMinutes);
            string formattedTime = StringConstants.FormatRemainingTime(fullDuration);
            unlockTimerText.text = formattedTime;
        }

        private void SetGemText(int totalGems) => gemText.text = totalGems.ToString();

        public void SetBgImg(ChestType chestType)
        {
            foreach (var bgImg in chestBgImages)
            {
                if (bgImg.chestType == chestType)
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

        public void StartUnlockTimer(Func<TimeSpan> getRemainingTime, Func<int> getGemCost, Action onFinished)
        {
            if (unlockTimerCoroutine != null)
                StopCoroutine(unlockTimerCoroutine);

            unlockTimerCoroutine = StartCoroutine(RunUnlockTimer(getRemainingTime, getGemCost, onFinished));
        }

        private IEnumerator RunUnlockTimer(Func<TimeSpan> getRemainingTime, Func<int> getGemCost, Action onFinished)
        {
            while (true)
            {
                TimeSpan remaining = getRemainingTime();
                unlockTimerText.text = StringConstants.FormatRemainingTime(remaining);

                int gemCost = getGemCost();
                SetGemText(gemCost);

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

        public void ResetTimer(int totalMinutes)
        {
            StopUnlockTimer();
            SetTimerText(totalMinutes);
        }

        public ChestView ChestView => chestView;

        public void OnDestroy()
        {
            chestSlotButton.onClick.RemoveAllListeners();
            StopUnlockTimer();
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