using ChestSystem.Chests;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Main
{
    public class GameService : MonoBehaviour
    {
        [SerializeField] private Transform chestSlotParent;
        [SerializeField] private ChestView chestViewPrefab;
        [SerializeField] private List<ChestScriptableObject> chestTypes;

        private ChestService chestService;

        private void Awake()
        {
            chestService = new ChestService();
        }

        private void Update()
        {
            chestService.UpdateAllChests(); // or drive it on timer/coroutine
        }
    }
}