using System;
using strange.extensions.mediation.impl;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Views.MainMenu
{
    public class SettingsView : EventView
    {
        /// <summary>
        /// Content
        /// </summary>
        [SerializeField] private GameObject content;

        /// <summary>
        /// Content
        /// </summary>
        [SerializeField] private Slider volume;

        /// <summary>
        /// Back to menu btn 
        /// </summary>
        [SerializeField] private Button backBtn;

        /// <summary>
        /// On load main menu
        /// </summary>
        public event Action OnLoadMainMenu;

        /// <summary>
        /// On load main menu
        /// </summary>
        public event Action<Slider> OnInitVolume;

        /// <summary>
        /// On change volume
        /// </summary>
        public event Action<float> OnChangeVolume;

        protected override void Start()
        {
            OnInitVolume?.Invoke(volume);

            volume.onValueChanged.AddListener(vol => { OnChangeVolume?.Invoke(vol); });


            backBtn.onClick.AddListener(() =>
            {
                content.SetActive(false);
                OnLoadMainMenu?.Invoke();
            });
        }

        /// <summary>
        /// Show content
        /// </summary>
        public void ShowContent()
        {
            content.SetActive(true);
        }
    }
}