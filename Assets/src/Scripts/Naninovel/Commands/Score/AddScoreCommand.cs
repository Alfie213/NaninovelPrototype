using Naninovel;
using MyGame.Services;
using UnityEngine;

namespace MyGame.NaninovelCommands
{
    [CommandAlias("addScore")]
    public class AddScoreCommand : Command
    {
        [ParameterAlias(NamelessParameterAlias)]
        public StringParameter ScoreAmount;

        private const int DefaultScoreValue = 0;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            int scoreValue = ScoreAmount.HasValue ? int.Parse(ScoreAmount.Value) : DefaultScoreValue;
            Debug.Log($"Adding score: {scoreValue}");
            ScoreService.Instance.AddScore(scoreValue);
            Debug.Log($"Current score: {ScoreService.Instance.CurrentScore}");
            return UniTask.CompletedTask;
        }
    }
}