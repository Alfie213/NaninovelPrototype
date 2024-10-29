using Naninovel;
using MyGame.Services;
using UnityEngine;

namespace MyGame.NaninovelCommands
{
    [CommandAlias("resetScore")]
    public class ResetScoreCommand : Command
    {
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            ScoreService.Instance.ResetScore();
            Debug.Log("Score reset.");
            return UniTask.CompletedTask;
        }
    }
}