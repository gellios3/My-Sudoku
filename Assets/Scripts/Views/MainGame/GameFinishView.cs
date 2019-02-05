using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Views.MainGame
{
    public class GameFinishView : EventView
    {
        [SerializeField] private GameObject content;
        [SerializeField] private Text resultText;
        [SerializeField] private Text timeValueText;
        [SerializeField] private Text movesValueText;

        public void Display(bool won, int elapsedSeconds, int moves)
        {
            resultText.text = won ? "Won!" : "Lose!";

            timeValueText.text = $"{elapsedSeconds / 60:D2}:{elapsedSeconds % 60:D2}";
            movesValueText.text = $"{moves:D3}";

            content.SetActive(true);
        }

        public void Hide()
        {
            content.SetActive(false);
        }

        public void ButtonPlayAgain()
        {
            LoadGameScene();
        }

        public void ButtonBackToMenu()
        {
            LoadMainMenuScene();
        }

        private static void LoadGameScene()
        {
            SceneManager.LoadSceneAsync("MainGame");
        }

        private static void LoadMainMenuScene()
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}