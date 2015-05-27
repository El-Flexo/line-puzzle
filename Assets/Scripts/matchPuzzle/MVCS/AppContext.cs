using UnityEngine;
using matchPuzzle.MVCS.controller.signal;
using matchPuzzle.MVCS.model;
using matchPuzzle.MVCS.model.level;
using matchPuzzle.MVCS.model.level.chain;
using matchPuzzle.MVCS.controller;
using matchPuzzle.MVCS.view.game.level;
using matchPuzzle.core.UI;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;
using matchPuzzle.MVCS.view.UI.main;

namespace matchPuzzle.MVCS
{
    public class AppContext : MVCSContext
    {
        readonly EntryPoint entryPoint;

        public AppContext(EntryPoint _view) : base(_view, true)
        {
            entryPoint = _view;
        }

        protected override void addCoreComponents()
        {
            // up signals
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        public override void Launch()
        {
            injectionBinder.GetInstance<StratupSignal>().Dispatch();
        }

        protected override void mapBindings()
        {
            mapCommands();
            mapSignals();
            mapModels();
            mapMediators();
            mapUIMediators();
            mapOthers();
        }

        void mapCommands()
        {
            commandBinder.Bind<StratupSignal>().To<StartupCommand>();
            commandBinder.Bind<StartLevelSignal>().To<StartLevelCommand>();
        }

        void mapSignals()
        {
            injectionBinder.Bind<PinElementSignal>().ToSingleton();
            injectionBinder.Bind<UnpinElementSignal>().ToSingleton();
            injectionBinder.Bind<EliminateElementsSignal>().ToSingleton();
            injectionBinder.Bind<AddElementsSignal>().ToSingleton();
            injectionBinder.Bind<MoveElementsSignal>().ToSingleton();
        }

        void mapModels()
        {
            injectionBinder.Bind<RandomProxy>().To(new RandomProxy(123));
            injectionBinder.Bind<IElementGenerator>().To<RandomElementGenerator>().ToSingleton();
            injectionBinder.Bind<ILevelModel>().To<LevelModel>();
            injectionBinder.Bind<IChainModel>().To<ChainModel>();

            injectionBinder.Bind<IElementsDefModel>().To<ElementsDefModel>().ToSingleton();
            injectionBinder.Bind<ILevelListModel>().To<LevelListModel>().ToSingleton();
        }

        void mapMediators()
        {
            mediationBinder.Bind<LevelView>().To<LevelMediator>();
        }

        void mapUIMediators()
        {
            mediationBinder.Bind<ScreenMainView>().To<ScreenMainMediator>();
        }

        void mapOthers()
        {
            injectionBinder.Bind<GameObject>().To(entryPoint.World).ToName(EntryPoint.Container.World);
            injectionBinder.Bind<GameObject>().To(entryPoint.UI).ToName(EntryPoint.Container.UI);

            injectionBinder.Bind<UIMap>().ToSingleton();
            injectionBinder.Bind<UIManager>().ToSingleton();
        }
    }
}