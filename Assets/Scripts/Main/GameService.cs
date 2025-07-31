using ChestSystem.Events;
using ChestSystem.Chests;
using ChestSystem.Command;
using ChestSystem.Currency;
using ChestSystem.UI;
using ChestSystem.Utilities;
using UnityEngine;
using ChestSystem.Chests.ChestUnlockQueue;

namespace ChestSystem.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        // Serialized initial values
        [Header("Initial Currency Settings")]
        [SerializeField] private int initialCoins;
        [SerializeField] private int initialGems;

        // Services:
        public EventService EventService { get; private set; }
        public ChestService ChestService { get; private set; }
        public ChestUnlockQueueService ChestUnlockQueueService { get; private set; }
        public CurrencyService CurrencyService { get; private set; }
        public CommandInvoker CommandInvoker { get; private set; }

        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;

        protected override void Awake()
        {
            base.Awake();
            EventService = new EventService();
            ChestService = new ChestService();
            ChestUnlockQueueService = new ChestUnlockQueueService();
            CurrencyService = new CurrencyService();
            CommandInvoker = new CommandInvoker();
        }

        private void Start()
        {
            ChestService.Initialize();
            CurrencyService.Initialize(initialCoins, initialGems);
        }

        private void Update() => ChestService.UpdateChest();

        public void OnExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
            UnblockRaycasts();
            UIService.ShowMessagePopupUI(StringConstants.WebGLCloseGamePopup);
#else
            Application.Quit();
#endif
        }
    }
}