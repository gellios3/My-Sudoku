using System;
using System.Threading.Tasks;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Views.MainGame
{
    public class KeyboardNumericView : EventView
    {
        [SerializeField] private GameObject content;

        [SerializeField] private Button[] keyboardButtons;

        public GameObject Content => content;

        public BoardTileView activeTileView { get; set; }

        public bool readyToPress { private get; set; }

        public event Action<int> OnNumberPressed;

        protected override void Start()
        {
            for (var i = 0; i < keyboardButtons.Length; i++)
            {
                if (i <= 9)
                {
                    var index = i;
                    keyboardButtons[i].onClick.AddListener(() => { NumericButtonPressed(index);});
                }
                else
                {
                    keyboardButtons[i].onClick.AddListener(ReturnButtonPressed);
                }
            }
        }

        private void Update()
        {
            if (!gameObject.activeInHierarchy)
                return;
            if (Input.GetKeyDown(KeyCode.Keypad0)) NumericButtonPressed(0);
            if (Input.GetKeyDown(KeyCode.Keypad1)) NumericButtonPressed(1);
            if (Input.GetKeyDown(KeyCode.Keypad2)) NumericButtonPressed(2);
            if (Input.GetKeyDown(KeyCode.Keypad3)) NumericButtonPressed(3);
            if (Input.GetKeyDown(KeyCode.Keypad4)) NumericButtonPressed(4);
            if (Input.GetKeyDown(KeyCode.Keypad5)) NumericButtonPressed(5);
            if (Input.GetKeyDown(KeyCode.Keypad6)) NumericButtonPressed(6);
            if (Input.GetKeyDown(KeyCode.Keypad7)) NumericButtonPressed(7);
            if (Input.GetKeyDown(KeyCode.Keypad8)) NumericButtonPressed(8);
            if (Input.GetKeyDown(KeyCode.Keypad9)) NumericButtonPressed(9);
        }

        private void Hide()
        {
            Content.SetActive(false);
        }

        private void NumericButtonPressed(int value)
        {
            if (!readyToPress)
                return;
            OnNumberPressed?.Invoke(value);
        }

        public void ReturnButtonPressed()
        {
            if (!readyToPress)
                return;
            Hide();
        }
    }
}