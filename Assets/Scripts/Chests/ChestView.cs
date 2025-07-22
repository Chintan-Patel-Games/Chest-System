using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.Chests
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private Image chestImage;
        [SerializeField] private Animator chestAnimator;

        /// <summary>
        /// Set visual sprite and animation controller.
        /// </summary>
        public void SetVisuals(Sprite sprite, RuntimeAnimatorController animatorController)
        {
            chestImage.sprite = sprite;
            chestAnimator.runtimeAnimatorController = animatorController;

            // Reset animator state
            chestAnimator.Rebind();
            chestAnimator.Update(0f);
        }

        /// <summary>
        /// Plays the chest opening animation by setting "Open" parameter.
        /// </summary>
        public void PlayOpenAnimation()
        {
            if (chestAnimator != null && chestAnimator.runtimeAnimatorController != null)
                chestAnimator.SetBool("Open", true);
        }

        /// <summary>
        /// Optional: Reset chest to locked state by disabling "Open".
        /// </summary>
        public void ResetToLocked()
        {
            if (chestAnimator != null)
                chestAnimator.SetBool("Open", false);
        }
    }
}