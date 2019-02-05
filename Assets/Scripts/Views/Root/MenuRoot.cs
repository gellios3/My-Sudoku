using Contexts;
using strange.extensions.context.impl;
using UnityEngine;

namespace Views.Root
{
    public class MenuRoot : ContextView
    {
        [SerializeField] private GameObject _dontDestroy;

        private void Awake()
        {
            if (GameObject.FindWithTag("DontDestroy") == null)
            {
                Instantiate(_dontDestroy);
            }

            context = new MenuContext(this);
        }
    }
}