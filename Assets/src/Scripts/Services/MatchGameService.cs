using MemoryMatchGame;
using UnityEngine;

namespace MyGame.Services
{
    public class MatchGameService : MonoBehaviour
    {
        public static MatchGameService Instance { get; private set; }

        [SerializeField] private MemoryMatchGameManager memoryMatchGameManager;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void StartMatchGame()
        {
            memoryMatchGameManager.StartGame();
        }

        public void Handle_OnGameEnd()
        {
            Debug.Log("Handling");
        }
    }
}