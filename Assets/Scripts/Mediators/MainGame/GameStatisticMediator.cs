using Services;
using Signals.MainGame;
using UnityEngine;
using Views.MainGame;

namespace Mediators.MainGame
{
    public class GameStatisticMediator : TargetMediator<GameStatisticView>
    {
        /// <summary>
        /// Update moves signal
        /// </summary>
        [Inject]
        public UpdateMovesSignal UpdateMovesSignal { get; set; }

        /// <summary>
        /// Update timer signal
        /// </summary>
        [Inject]
        public UpdateTimerSignal UpdateTimerSignal { get; set; }

        /// <summary>
        /// Player starts service
        /// </summary>
        [Inject]
        public PlayerStartsService PlayerStartsService { get; set; }

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            UpdateTimerSignal.AddListener(timer => { View.TimerText.text = timer; });

            UpdateMovesSignal.AddListener(moves => { View.MoveCounterText.text = moves; });

            PlayerStartsService.Moves = 0;
            PlayerStartsService.ElapsedSeconds = 0;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PlayerStartsService.CancelLevel();
            }
        }
    }
}