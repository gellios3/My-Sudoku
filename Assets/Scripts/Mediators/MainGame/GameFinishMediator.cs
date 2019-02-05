using Signals.MainGame;
using Views.MainGame;

namespace Mediators.MainGame
{
    public class GameFinishMediator : TargetMediator<GameFinishView>
    {
        /// <summary>
        /// On show finish game signal
        /// </summary>
        [Inject]
        public OnShowFinishGameSignal OnShowFinishGameSignal { get; set; }

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            OnShowFinishGameSignal.AddListener(View.Display);
        }
    }
}