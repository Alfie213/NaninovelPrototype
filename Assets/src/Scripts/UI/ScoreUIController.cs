using UnityEngine;
using TMPro;
using MyGame.Services;

namespace MyGame.UI
{
    public class ScoreUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private void OnEnable()
        {
            ScoreService.Instance.OnScoreUpdated += UpdateScoreUI;
            UpdateScoreUI(ScoreService.Instance.CurrentScore);
        }

        private void OnDisable()
        {
            ScoreService.Instance.OnScoreUpdated -= UpdateScoreUI;
        }

        private void UpdateScoreUI(int newScore)
        {
            scoreText.text = $"Score: {newScore}";
        }
    }
}
