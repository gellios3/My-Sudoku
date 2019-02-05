using Signals.MainMenu;
using Views.MainMenu;

namespace Mediators.MainMenu
{
    public class BackgroundMusicMediator : TargetMediator<BackgroundMusicView>
    {
        /// <summary>
        /// On change volume signal
        /// </summary>
        [Inject]
        public OnChangeVolumeSignal OnChangeVolumeSignal { get; set; }

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            OnChangeVolumeSignal.AddListener(volume => { View.Source.volume = volume; });
        }
    }
}