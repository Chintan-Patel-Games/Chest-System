using UnityEngine;

namespace ChestSystem.Sound
{
    [CreateAssetMenu(fileName = "SoundScriptableObject", menuName = "ScriptableObjects/SoundScriptableObject")]
    public class SoundSO : ScriptableObject
    {
        public Sounds[] audioList;
    }

    [System.Serializable]
    public struct Sounds
    {
        public SoundType soundType;
        public AudioClip audio;
    }
}