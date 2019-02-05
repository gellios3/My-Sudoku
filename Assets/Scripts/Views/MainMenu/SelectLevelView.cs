using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Views.MainMenu
{
    public class SelectLevelView : EventView
    {
        /// <summary>
        /// Content
        /// </summary>
        [SerializeField] private GameObject content;

        /// <summary>
        /// Current level buttons
        /// </summary>
        [SerializeField] private Button[] currentLevelBtn;

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
        public event Action<int> OnLoadMainGame;

        protected override void Start()
        {
            for (var i = 0; i < currentLevelBtn.Length; i++)
            {
                var levelNum = i;
                currentLevelBtn[i].onClick.AddListener(() => OnLoadMainGame?.Invoke(levelNum));
            }

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