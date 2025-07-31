using UnityEngine;

namespace ChestSystem.Chests
{
    [CreateAssetMenu(fileName = "ChestScriptableObject", menuName = "ScriptableObjects/ChestScriptableObject")]
    public class ChestScriptableObject : ScriptableObject
    {
        public Sprite chestSprite;
        public RuntimeAnimatorController animatorController;

        public ChestType chestType;
        public ChestUnlockTimer unlockDurationMinutes;

        public RewardRange coins;
        public RewardRange gems;

        [System.Serializable]
        public struct RewardRange
        {
            public int min;
            public int max;
        }
    }
}