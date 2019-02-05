using Services;
using Signals.MainMenu;
using Views.MainMenu;

namespace Mediators.MainMenu
{
    public class SettingsMediator : TargetMediator<SettingsView>
    {
        /// <summary>
        /// On load settings signal
        /// </summary>
        [Inject]
        public OnLoadSettingsSignal OnLoadSettingsSignal { get; set; }

        /// <summary>
        /// On load main menu signal
        /// </summary>
        [Inject]
        public OnLoadMainMenuSignal OnLoadMainMenuSignal { get; set; }

        /// <summary>
        /// On change volume signal
        /// </summary>
        [Inject]
        public OnChangeVolumeSignal OnChangeVolumeSignal { get; set; }

        /// <summary>
        /// Player settings service
        /// </summary>
        [Inject]
        public PlayerSettingsService PlayerSettingsService { get; set; }

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            OnLoadSettingsSignal.AddListener(() => { View.ShowContent(); });

            View.OnInitVolume += slider =>
            {
                var volume = PlayerSettingsService.InitVolume();
                slider.value = volume;
                OnChangeVolumeSignal.Dispatch(volume);
            };

            View.OnChangeVolume += volume =>
            {
                PlayerSettingsService.UpdateVolume(volume);
                OnChangeVolumeSignal.Dispatch(volume);
            };

            View.OnLoadMainMenu += () => { OnLoadMainMenuSignal.Dispatch(); };
        }
    }
}