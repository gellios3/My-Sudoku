using strange.extensions.command.impl;
using Services;
using UnityEngine;

namespace Commands
{
    public class BoardFinishedLoadingCommand : Command
    {
        /// <summary>
        /// Level manager
        /// </summary>
        [Inject]
        public LevelsManager LevelsManager { get; set; }

        /// <summary>
        /// Level manager
        /// </summary>
        [Inject]
        public PlayerSettingsService PlayerSettingsService { get; set; }

        /// <summary>
        /// Board service
        /// </summary>
        [Inject]
        public BoardService BoardService { get; set; }

        /// <summary>
        /// Execute command
        /// </summary>
        public override void Execute()
        {
            if (LevelsManager.SelectedLevel == null)
            {
                LevelsManager.SelectedLevel = LevelsManager.levels[PlayerSettingsService.CurrentLevel];
            }

            BoardService.SetLevel(LevelsManager.SelectedLevel.Board);
        }
    }
}