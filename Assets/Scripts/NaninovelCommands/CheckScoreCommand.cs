using Naninovel;
using MyGame.Services;
using UnityEngine;

namespace MyGame.NaninovelCommands
{
    [CommandAlias("checkScore")]
    public class CheckScoreCommand : Command
    {
        [ParameterAlias("minScore"), ParameterDefaultValue("0")]
        public IntegerParameter MinScore;

        [ParameterAlias("var")]
        public StringParameter ResultVariable;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            bool hasEnoughScore = ScoreService.Instance.CurrentScore >= MinScore;

            if (!string.IsNullOrEmpty(ResultVariable))
            {
                var manager = Engine.GetService<CustomVariableManager>();
                manager.SetVariableValue(ResultVariable, hasEnoughScore ? "true" : "false");
            }
            return UniTask.CompletedTask;
        }
    }
}
