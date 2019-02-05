using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Views.MainGame
{
    public class BoardTileView : EventView
    {
        private static readonly Color ConstantBackground = Color.gray;


        public delegate void ValueChangedDelegate();

        public event ValueChangedDelegate ValueChanged;

        public event Action<BoardTileView> OnTilePressed;

        [SerializeField] private Text valueField;

        [SerializeField] private Button tileBtn;

        private bool _constant;
        private int _value;

        public int Value
        {
            get => _value;
            set
            {
                _value = value;

                if (value != 0)
                {
                    valueField.text = $"{value}";
                    if (ValueChanged != null && !_constant) ValueChanged();
                }
                else valueField.text = "";
            }
        }

        protected override void Start()
        {
            tileBtn.onClick.AddListener(() =>
            {
                if (_constant)
                    return;
                OnTilePressed?.Invoke(this);
            });
        }

        public void Clear()
        {
            Value = 0;
            _constant = false;
            GetComponent<Image>().color = Color.white;
        }

        public void SetConstantValue(int value)
        {
            if (value <= 0) 
                return;
            _constant = true;
            Value = value;
            GetComponent<Image>().color = ConstantBackground;
        }

    }
}