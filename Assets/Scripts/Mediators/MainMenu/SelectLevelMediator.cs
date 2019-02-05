using System.Threading.Tasks;
using Services;
using Signals.MainMenu;
using UnityEngine.SceneManagement;
using Views.MainMenu;

namespace Mediators.MainMenu
{
    public class SelectLevelMediator : TargetMediator<SelectLevelView>
    {
        /// <summary>
        /// On load select level signal
        /// </summary>
        [Inject]
        public OnLoadSelectLevelSignal OnLoadSelectLevelSignal { get; set; }

        /// <summary>
        /// On load main menu signal
        /// </summary>
        [Inject]
        public OnLoadMainMenuSignal OnLoadMainMenuSignal { get; set; }

        /// <summary>
        /// Player settings service
        /// </summary>
        [Inject]
        public PlayerSettingsService PlayerSettingsService { get; set; }
        
        /// <summary>
        /// Level manager
        /// </summary>
        [Inject]
        public LevelsManager LevelsManager { get; set; }

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            View.OnLoadMainMenu += () => { OnLoadMainMenuSignal.Dispatch(); };
            View.OnLoadMainGame += level =>
            {
                PlayerSettingsService.CurrentLevel = level;
                LoadGameScene();
            };

            OnLoadSelectLevelSignal.AddListener(() =>
            {
                View.ShowContent();
                LevelsManager.InitLevels();
            });
        }

        private static void LoadGameScene()
        {
            SceneManager.LoadSceneAsync("MainGame");
        }
    }
}