using UnityEngine;
using System;

namespace MyGame.Services
{
    public class ScoreService : MonoBehaviour
    {
        public static ScoreService Instance { get; private set; }

        public event Action<int> OnScoreUpdated;

        private int _currentScore;

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

        public int CurrentScore => _currentScore;

        public void AddScore(int amount)
        {
            _currentScore += amount;
            OnScoreUpdated?.Invoke(_currentScore);
        }

        public void ResetScore()
        {
            _currentScore = 0;
            OnScoreUpdated?.Invoke(_currentScore);
        }
    }
}
