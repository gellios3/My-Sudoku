using System;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class PlayerSettingsService
    {

        /// <summary>
        /// Best score
        /// </summary>
        public int CurrentLevel { get; set; }

        /// <summary>
        /// Best score
        /// </summary>
        public float Volume { get; private set; }

        /// <summary>
        /// Has return to menu
        /// </summary>
        public bool HasReturnToSelectLevel { get; set; }

        /// <summary>
        /// Update volume
        /// </summary>
        /// <returns></returns>
        public void UpdateVolume(float volume)
        {
            Volume = volume;
            PlayerPrefs.SetFloat("userVolume", Volume);
        }

        /// <summary>
        /// Init volume
        /// </summary>
        /// <returns></returns>
        public float InitVolume()
        {
            Volume = PlayerPrefs.GetFloat("userVolume", 1);
            return Volume;
        }
        
    }
}