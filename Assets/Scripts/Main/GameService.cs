using ChestSystem.Chests;
using ChestSystem.Chests.ChestUnlockQueue;
using ChestSystem.Command;
using ChestSystem.Currency;
using ChestSystem.Events;
using ChestSystem.Sound;
using ChestSystem.UI;
using ChestSystem.Utilities;
using UnityEngine;

namespace ChestSystem.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        // Serialized initial values
        [Header("Initial Currency Settings")]
        [SerializeField] private int initialCoins;
        [SerializeField] private int initialGems;

        // Services:
        public SoundService SoundService { get; private set; }
        public EventService EventService { get; private set; }
        public ChestService ChestService { get; private set; }
        public ChestUnlockQueueService ChestUnlockQueueService { get; private set; }
        public CurrencyService CurrencyService { get; private set; }
        public CommandInvoker CommandInvoker { get; private set; }

        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;

        [SerializeField] private SoundSO soundSO;

        [Header("AudioSource References")]
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource bgMusicSource;

        protected override void Awake()
        {
            base.Awake();
            EventService = new EventService();
            SoundService = new SoundService(soundSO, sfxSource, bgMusicSource);
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
                UIService.ShowMessagePopupUI(StringConstants.WebGLCloseGamePopup);
            #else
                Application.Quit();
            #endif
        }
    }
}