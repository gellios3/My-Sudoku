using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Views.MainGame
{
    public class GameStatisticView: EventView
    {
        [SerializeField] private Text timerText;
        [SerializeField] private Text moveCounterText;

        public Text MoveCounterText => moveCounterText;
        public Text TimerText => timerText;
    }
}