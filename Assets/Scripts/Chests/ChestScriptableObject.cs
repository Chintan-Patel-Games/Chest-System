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

        public CoinsRange coins;
        public GemsRange gems;

        [System.Serializable]
        public struct CoinsRange
        {
            public int min, max;
        }

        [System.Serializable]
        public struct GemsRange
        {
            public int min, max;
        }
    }
}