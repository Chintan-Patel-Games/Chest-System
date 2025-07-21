using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.Chests
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private Image chestImage;
        [SerializeField] private Animator chestAnimator;

        public void SetVisuals(Sprite sprite, RuntimeAnimatorController animatorController)
        {
            chestImage.sprite = sprite;
            chestAnimator.runtimeAnimatorController = animatorController;
        }
    }
}