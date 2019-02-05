using Mediators.MainMenu;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Services;
using Signals.MainMenu;
using UnityEngine;
using Views.MainMenu;

namespace Contexts
{
    public class DontDestroyContext : MVCSContext
    {
        public DontDestroyContext(MonoBehaviour view) : base(view)
        {
            _instance = this;
        }

        public DontDestroyContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
            _instance = this;
        }

        private static DontDestroyContext _instance;

        public static T Get<T>()
        {
            return _instance.injectionBinder.GetInstance<T>();
        }

        /// <inheritdoc />
        /// <summary>
        /// Unbind the default EventCommandBinder and rebind the SignalCommandBinder
        /// </summary>
        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        /// <summary>
        /// Override Start so that we can fire the StartSignal 
        /// </summary>
        /// <returns></returns>
        public override IContext Start()
        {
            base.Start();
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Override Bindings map
        /// </summary>
        protected override void mapBindings()
        {
            // init Signals
            injectionBinder.Bind<OnChangeVolumeSignal>().ToSingleton().CrossContext();
            // Init commands

            // Init services
            injectionBinder.Bind<PlayerSettingsService>().ToSingleton().CrossContext();
            injectionBinder.Bind<LevelsManager>().ToSingleton().CrossContext();
            injectionBinder.Bind<SudokuSolverService>().ToSingleton().CrossContext();

            // Init mediators
            mediationBinder.Bind<BackgroundMusicView>().To<BackgroundMusicMediator>();
        }
    }
}