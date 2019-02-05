using Signals.MainMenu;
using Views.MainMenu;

namespace Mediators.MainMenu
{
    public class MainMenuMediator : TargetMediator<MainMenuView>
    {

        /// <summary>
        /// On load select level signal
        /// </summary>
        [Inject]
        public OnLoadSelectLevelSignal OnLoadSelectLevelSignal { get; set; }

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
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {

            OnLoadMainMenuSignal.AddListener(() => { View.ShowContent(); });
            OnLoadSelectLevelSignal.AddListener(() =>
            {
                View.HideContent();
            });
            
            View.OnLoadSelectLevel += () => { OnLoadSelectLevelSignal.Dispatch(); };
            View.OnLoadSettings += () => { OnLoadSettingsSignal.Dispatch(); };
        }
    }
}