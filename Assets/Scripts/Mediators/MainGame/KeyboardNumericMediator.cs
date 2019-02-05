using System.Threading.Tasks;
using Services;
using Signals.MainGame;
using UnityEngine;
using Views.MainGame;

namespace Mediators.MainGame
{
    public class KeyboardNumericMediator : TargetMediator<KeyboardNumericView>
    {
        /// <summary>
        /// Player starts service
        /// </summary>
        [Inject]
        public PlayerStartsService PlayerStartsService { get; set; }

        /// <summary>
        /// Show keyboard signal
        /// </summary>
        [Inject]
        public ShowKeyboardSignal ShowKeyboardSignal { get; set; }

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            View.OnNumberPressed += value =>
            {
                View.activeTileView.Value = value;
                PlayerStartsService.Moves++;
                View.Content.SetActive(false);
            };

            ShowKeyboardSignal.AddListener(DisplayKeyboard);
        }

        /// <summary>
        /// Display keyboard
        /// </summary>
        /// <param name="pressedTileView"></param>
        private void DisplayKeyboard(BoardTileView pressedTileView)
        {
            if (!PlayerStartsService.HasPlaying)
                return;
            View.Content.SetActive(true);
            View.activeTileView = pressedTileView;
            View.readyToPress = false;
            WaitToReleaseButton();
        }

        /// <summary>
        /// Wait to release button -
        /// Delay for mouse and touch input, prevent from accidentally select board tile and keyboard number button at once
        /// </summary>
        private async void WaitToReleaseButton()
        {
            while (Input.anyKey || Input.touchCount != 0)
            {
                await Task.Delay(100);
            }

            await Task.Delay(50);
            View.readyToPress = true;
        }
    }
}