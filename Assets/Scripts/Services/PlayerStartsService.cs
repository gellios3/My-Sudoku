using Signals.MainGame;
using System.Threading.Tasks;

namespace Services
{
    public class PlayerStartsService
    {
        /// <summary>
        /// Update moves signal
        /// </summary>
        [Inject]
        public UpdateMovesSignal UpdateMovesSignal { get; set; }
        
        /// <summary>
        /// On show finish game signal
        /// </summary>
        [Inject]
        public OnShowFinishGameSignal OnShowFinishGameSignal { get; set; }

        /// <summary>
        /// Update timer signal
        /// </summary>
        [Inject]
        public UpdateTimerSignal UpdateTimerSignal { get; set; }

        public bool HasPlaying { get;  set; }

        public bool HasGameOver { get; set; }

        private int _elapsedSeconds;

        public int ElapsedSeconds
        {
            get => _elapsedSeconds;
            set
            {
                _elapsedSeconds = value;
                UpdateTimerSignal.Dispatch($"{value / 60:D2}:{value % 60:D2}");
            }
        }

        private int _moves;

        public int Moves
        {
            get => _moves;
            set
            {
                _moves = value;
                UpdateMovesSignal.Dispatch($"{value:D3}");
            }
        }

        public void CancelLevel()
        {
//            if (gameFinish.isActiveAndEnabled)
//                return;
            if (HasGameOver)
                return;
            HasPlaying = false;
            OnShowFinishGameSignal.Dispatch(false, _elapsedSeconds, _moves);
        }

        public void WinLevel()
        {
            OnShowFinishGameSignal.Dispatch(true, _elapsedSeconds, _moves);
        }

        private void OnDestroy()
        {
            HasPlaying = false;
        }

        private async void StartTimer()
        {
            while (HasPlaying)
            {
                await Task.Delay(1000);
                ElapsedSeconds++;
            }
        }

        public void BoardReadyToPlay()
        {
            HasPlaying = true;
            ElapsedSeconds = 0;
            Moves = 0;

            StartTimer();
        }
    }
}