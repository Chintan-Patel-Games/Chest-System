using ChestSystem.Events;
using ChestSystem.Chests;
using ChestSystem.UI;
using ChestSystem.Utilities;
using System.Collections.Generic;
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

        [SerializeField] private ChestSlotView chestSlotPrefab;
        [SerializeField] private ChestView chestPrefab;
        [SerializeField] private Transform chestSlotTransform;
        [SerializeField] private List<ChestScriptableObject> chestSOs;

        protected override void Awake()
        {
            base.Awake();
            EventService = new EventService();
            ChestService = new ChestService(chestSlotPrefab, chestSlotTransform, chestPrefab);
        }
    }
}