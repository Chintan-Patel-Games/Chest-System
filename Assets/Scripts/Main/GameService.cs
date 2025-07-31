using ChestSystem.Events;
using ChestSystem.Chests;
using ChestSystem.Command;
using ChestSystem.UI;
using ChestSystem.Utilities;
using UnityEngine;
using ChestSystem.Currency;

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
        public CurrencyService CurrencyService { get; private set; }
        public CommandInvoker CommandInvoker { get; private set; }

        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;

        protected override void Awake()
        {
            base.Awake();
            EventService = new EventService();
            ChestService = new ChestService();
            CurrencyService = new CurrencyService();
            CommandInvoker = new CommandInvoker();
        }

        private void Start()
        {
            ChestService.Initialize();
            CurrencyService.Initialize(initialCoins, initialGems);
        }

        private void Update() => ChestService.UpdateChest();
    }
}