using System;
using strange.extensions.mediation.impl;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Views.MainMenu
{
    public class MainMenuView : EventView
    {
        /// <summary>
        /// Content
        /// </summary>
        [SerializeField] private GameObject content;

        /// <summary>
        /// Select level btn
        /// </summary>
        [SerializeField] private Button selectLevelBtn;

        /// <summary>
        /// Settings btn
        /// </summary>
        [SerializeField] private Button settingsBtn;

        /// <summary>
        /// Exit game btn
        /// </summary>
        [SerializeField] private Button exitGameBtn;

        /// <summary>
        /// Player settings
        /// </summary>
        [Inject]
        public PlayerSettingsService PlayerSettingsService { get; set; }

        /// <summary>
        /// On load select level
        /// </summary>
        public event Action OnLoadSelectLevel;

        /// <summary>
        /// On load settings
        /// </summary>
        public event Action OnLoadSettings;

        protected override void Start()
        {

            if (PlayerSettingsService.HasReturnToSelectLevel)
            {
                OnLoadSelectLevel?.Invoke();
            }

            selectLevelBtn.onClick.AddListener(() => { OnLoadSelectLevel?.Invoke(); });
            settingsBtn.onClick.AddListener(() =>
            {
                content.SetActive(false);
                OnLoadSettings?.Invoke();
            });
            exitGameBtn.onClick.AddListener(Application.Quit);
        }

        /// <summary>
        /// Show content
        /// </summary>
        public void ShowContent()
        {
            content.SetActive(true);
        }

        /// <summary>
        /// Hide content
        /// </summary>
        public void HideContent()
        {
            content.SetActive(false);
        }
    }
}