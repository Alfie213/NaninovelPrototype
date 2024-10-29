using Naninovel;
using MyGame.Services;

namespace MyGame.NaninovelCommands
{
    [CommandAlias("matchGame")]
    public class MatchGameCommand : Command
    {
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            MatchGameService.Instance.StartMatchGame();
            return UniTask.CompletedTask;
        }
    }
}