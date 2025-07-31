using ChestSystem.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.Chests
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private Image chestImage;
        [SerializeField] private Animator chestAnimator;

        public void Initialize(ChestScriptableObject data)
        {
            chestImage.sprite = data.chestSprite;
            chestAnimator.runtimeAnimatorController = data.animatorController;
        }

        public void PlayOpenAnimation() => SetAnimationBool(true);

        public void ResetToLocked() => SetAnimationBool(false);

        private void SetAnimationBool(bool isOpen) => chestAnimator.SetBool(StringConstants.OpenAnimParam, isOpen);
    }
}