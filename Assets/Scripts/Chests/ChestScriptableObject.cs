using UnityEngine;

namespace ChestSystem.Chests
{
    [CreateAssetMenu(fileName = "ChestScriptableObject", menuName = "ScriptableObjects/ChestScriptableObject")]
    public class ChestScriptableObject : ScriptableObject
    {
        public ChestView chestPrefab;
        public ChestType chestType;
        public ChestUnlockTimer unlockDurationMinutes;
        public int coinMin;
        public int coinMax;
        public int gemMin;
        public int gemMax;
    }
}