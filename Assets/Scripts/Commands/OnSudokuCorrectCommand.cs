using strange.extensions.command.impl;
using Services;

namespace Commands
{
    public class OnSudokuCorrectCommand : Command
    {
        /// <summary>
        /// Player starts service
        /// </summary>
        [Inject]
        public PlayerStartsService PlayerStartsService { get; set; }

        /// <summary>
        /// Execute command
        /// </summary>
        public override void Execute()
        {
            PlayerStartsService.HasPlaying = false;
            PlayerStartsService.WinLevel();
        }
    }
}