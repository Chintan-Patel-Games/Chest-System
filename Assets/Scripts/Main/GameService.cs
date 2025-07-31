using ChestSystem.Events;
using ChestSystem.Chests;
using ChestSystem.UI;
using ChestSystem.Utilities;
using UnityEngine;

namespace ChestSystem.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        // Services:
        public EventService EventService { get; private set; }
        public ChestService ChestService { get; private set; }

        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;

        protected override void Awake()
        {
            base.Awake();
            EventService = new EventService();
            ChestService = new ChestService();
        }

        private void Start()
        {
            // Initialize services
            ChestService.Initialize();
        }
    }
}