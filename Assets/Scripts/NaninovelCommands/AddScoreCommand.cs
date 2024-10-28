using Naninovel;
using MyGame.Services;
using UnityEngine;

namespace MyGame.NaninovelCommands
{
    [CommandAlias("addScore")]
    public class AddScoreCommand : Command
    {
        [ParameterAlias("amount"), ParameterDefaultValue("1")]
        public StringParameter ScoreAmount;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            int scoreValue = ScoreAmount.HasValue ? int.Parse(ScoreAmount.Value) : 1;
            Debug.Log($"Adding score: {scoreValue}");
            ScoreService.Instance.AddScore(scoreValue);
            Debug.Log($"Current score: {ScoreService.Instance.CurrentScore}");
            return UniTask.CompletedTask;
        }
    }
}