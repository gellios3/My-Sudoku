using strange.extensions.mediation.impl;
using UnityEngine;

namespace Views.MainMenu
{
    public class BackgroundMusicView : EventView
    {
        [SerializeField] private AudioSource _source;

        public AudioSource Source => _source;
    }
}