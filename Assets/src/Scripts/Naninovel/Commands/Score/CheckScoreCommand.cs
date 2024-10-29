using Naninovel;
using MyGame.Services;

namespace MyGame.NaninovelCommands
{
    [CommandAlias("checkScore")]
    public class CheckScoreCommand : Command
    {
        [ParameterAlias(NamelessParameterAlias)]
        public StringParameter MinScore;

        [ParameterAlias("var")]
        public StringParameter ResultVariable;

        private const int MinScoreDefaultValue = 0;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            int minScoreValue = MinScore.HasValue ? int.Parse(MinScore.Value) : MinScoreDefaultValue;
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