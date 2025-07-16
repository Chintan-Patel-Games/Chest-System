using ChestSystem.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.Chests
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer chestGraphic;
        [SerializeField] private Animator chestAnimator;
        [SerializeField] private Text timerText;
        [SerializeField] private Button chestButton;

        public ChestController Controller { get; private set; }

        public void Initialize()
        {
            chestButton.onClick.AddListener(OnChestClicked);
            UpdateView();
        }

        public void SetController(ChestController controllerToSet) => Controller = controllerToSet;

        public void UpdateView()
        {
            ChestState state = Controller.GetChestState();

            switch (state)
            {
                case ChestState.LOCKED:
                    timerText.text = StringConstants.ChestLocked;
                    break;
                case ChestState.UNLOCKING:
                    timerText.text = Controller.GetRemainingTimeFormatted();
                    break;
                case ChestState.UNLOCKED:
                    timerText.text = StringConstants.ChestUnlocked;
                    break;
                case ChestState.OPENED:
                    timerText.text = StringConstants.ChestOpened;
                    break;
            }
        }

        private void OnChestClicked() => Controller.OnChestClicked();
    }
}