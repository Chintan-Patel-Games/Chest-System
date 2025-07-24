using UnityEngine;

namespace ChestSystem.UI
{
    public class UIService : MonoBehaviour
    {
        [Header("Gameplay UI")]
        private GameplayUIController gameplayController;
        [SerializeField] private GameplayUIView gameplayView;

        private void Start()
        {
            gameplayController = new GameplayUIController(gameplayView);
        }
    }
}