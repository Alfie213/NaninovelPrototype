using Naninovel;
using MyGame.Services;
using UnityEngine;

namespace MyGame.NaninovelCommands
{
    [CommandAlias("addScore")]
    public class AddScoreCommand : Command
    {
        [ParameterAlias("amount"), ParameterDefaultValue("1")]
        public IntegerParameter ScoreAmount;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            Debug.Log("VAR");
            Debug.Log(ScoreAmount);
            if (ScoreAmount.HasValue)
            {
                ScoreService.Instance.AddScore(ScoreAmount.Value);
                Debug.Log(ScoreService.Instance.CurrentScore);
            }
            return UniTask.CompletedTask;
        }
    }
}
