using Commands;
using Mediators.MainGame;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Services;
using Signals.MainGame;
using UnityEngine;
using Views.MainGame;

namespace Contexts
{
    public class MainGameContext : MVCSContext
    {
        public MainGameContext(MonoBehaviour view) : base(view)
        {
            _instance = this;
        }

        public MainGameContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
            _instance = this;
        }

        private static MainGameContext _instance;

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
            injectionBinder.Bind<GameOverSignal>().ToSingleton();
            injectionBinder.Bind<CompleteLevelSignal>().ToSingleton();
            injectionBinder.Bind<UpdateMovesSignal>().ToSingleton();
            injectionBinder.Bind<UpdateTimerSignal>().ToSingleton();
            injectionBinder.Bind<ShowKeyboardSignal>().ToSingleton(); 
            injectionBinder.Bind<OnShowFinishGameSignal>().ToSingleton();

            // Init commands
            commandBinder.Bind<BoardFinishedLoadingSignal>().To<BoardFinishedLoadingCommand>();
            commandBinder.Bind<OnSudokuCorrectSignal>().To<OnSudokuCorrectCommand>();

            // Init services
            injectionBinder.Bind<PlayerStartsService>().ToSingleton(); 
            injectionBinder.Bind<BoardService>().ToSingleton();
    

            // Init mediators
            mediationBinder.Bind<GameStatisticView>().To<GameStatisticMediator>();
            mediationBinder.Bind<KeyboardNumericView>().To<KeyboardNumericMediator>();
            mediationBinder.Bind<BoardView>().To<BoardMediator>();
            mediationBinder.Bind<BoardTileView>().To<BoardTileMediator>();
            mediationBinder.Bind<GameFinishView>().To<GameFinishMediator>();
        }
    }
}