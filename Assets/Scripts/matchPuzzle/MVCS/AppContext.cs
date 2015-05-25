using System.Reflection.Emit;
using UnityEngine;
using matchPuzzle.MVCS.controller.signal;
using matchPuzzle.MVCS.model;
using matchPuzzle.MVCS.model.level;
using matchPuzzle.MVCS.model.level.chain;
using matchPuzzle.MVCS.model.level.provider;
using matchPuzzle.MVCS.view.game.level;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;

namespace matchPuzzle.MVCS {
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

        }

        void mapSignals()
        {
            injectionBinder.Bind<PinElementSignal>().ToSingleton();
            injectionBinder.Bind<UnpinElementSignal>().ToSingleton();
            injectionBinder.Bind<EliminateElementsSignal>().ToSingleton();
            injectionBinder.Bind<AddElementsSignal>().ToSingleton();
        }

        void mapModels()
        {
            injectionBinder.Bind<RandomProxy>().To(new RandomProxy(123));
            injectionBinder.Bind<IElementGenerator>().To<RandomElementGenerator>().ToSingleton();
            injectionBinder.Bind<ILevelProvider>().To<DefLevelProvider>().ToSingleton();
            injectionBinder.Bind<ILevelModel>().To<LevelModel>();
            injectionBinder.Bind<IChainModel>().To<ChainModel>();
        }

        void mapMediators()
        {
            mediationBinder.Bind<LevelView>().To<LevelMediator>();
        }

        void mapUIMediators()
        {

        }

        void mapOthers()
        {
            injectionBinder.Bind<GameObject>().To(entryPoint.World).ToName(EntryPoint.Container.World);
            injectionBinder.Bind<GameObject>().To(entryPoint.UI).ToName(EntryPoint.Container.UI);
        }
    }
}