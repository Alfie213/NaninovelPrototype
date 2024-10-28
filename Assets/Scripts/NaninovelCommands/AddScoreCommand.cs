using Naninovel;
using MyGame.Services;

namespace MyGame.NaninovelCommands
{
    [CommandAlias("addScore")]
    public class AddScoreCommand : Command
    {
        [ParameterAlias("amount"), ParameterDefaultValue("1")]
        public IntegerParameter ScoreAmount;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            if (ScoreAmount.HasValue)
            {
                ScoreService.Instance.AddScore(ScoreAmount.Value);
            }
            return UniTask.CompletedTask;
        }
    }
}
