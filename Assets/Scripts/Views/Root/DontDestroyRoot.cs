using Contexts;
using strange.extensions.context.impl;
using UnityEngine;

namespace Views.Root
{
    public class DontDestroyRoot : ContextView
    {
        protected void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
        
        private void Awake()
        {
            context = new DontDestroyContext(this);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}