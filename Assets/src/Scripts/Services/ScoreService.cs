using UnityEngine;
using System;

namespace MyGame.Services
{
    public class ScoreService : MonoBehaviour
    {
        public static ScoreService Instance { get; private set; }

        public event Action<int> OnScoreUpdated;

        private int currentScore;

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

        public int CurrentScore => currentScore;

        public void AddScore(int amount)
        {
            currentScore += amount;
            OnScoreUpdated?.Invoke(currentScore);
        }

        public void ResetScore()
        {
            currentScore = 0;
            OnScoreUpdated?.Invoke(currentScore);
        }
    }
}
