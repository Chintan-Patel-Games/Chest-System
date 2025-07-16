using UnityEngine;

namespace ChestSystem.Chests
{
    [CreateAssetMenu(fileName = "ChestScriptableObject", menuName = "ScriptableObjects/ChestScriptableObject")]
    public class ChestScriptableObject : ScriptableObject
    {
        public string chestName;
        public GameObject chestPrefab;
        public ChestType chestType;
        public ChestUnlockTimer unlockTimer;
        public AnimatorOverrideController chestAnimator;
    }
}