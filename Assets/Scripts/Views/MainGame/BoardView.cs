using System;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Views.MainGame
{
    public class BoardView : EventView
    {
        [SerializeField] public bool constantBoard; //Constant board in select level submenu

        public event Action OnBoardReadyToPlay;

        protected override void Start()
        {
            OnBoardReadyToPlay?.Invoke();
        }
    }
}