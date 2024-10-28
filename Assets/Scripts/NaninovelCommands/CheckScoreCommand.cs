using Naninovel;
using MyGame.Services;

namespace MyGame.NaninovelCommands
{
    [CommandAlias("checkScore")]
    public class CheckScoreCommand : Command
    {
        [ParameterAlias("minScore"), ParameterDefaultValue("0")]
        public StringParameter MinScore;

        [ParameterAlias("var")]
        public StringParameter ResultVariable;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            int minScoreValue = MinScore.HasValue ? int.Parse(MinScore.Value) : 0;
            bool hasEnoughScore = ScoreService.Instance.CurrentScore >= minScoreValue;

            if (!string.IsNullOrEmpty(ResultVariable))
            {
                var manager = Engine.GetService<CustomVariableManager>();
                manager.SetVariableValue(ResultVariable, hasEnoughScore ? "true" : "false");
            }
            return UniTask.CompletedTask;
        }
    }
}