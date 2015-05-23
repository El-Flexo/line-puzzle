using UnityEngine;
using matchPuzzle.MVCS.controller.signal;
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
            injectionBinder.GetInstance<InitializeSignal>().Dispatch();
        }

        protected override void mapBindings()
        {
            mapCommands();
            mapSignals();
            mapModels();
            mapMediators();
            mapUIMediators();
            mapServices();
            mapOthers();
        }

        void mapCommands()
        {

        }

        void mapSignals()
        {

        }

        void mapModels()
        {

        }

        void mapMediators()
        {

        }

        void mapUIMediators()
        {

        }

        void mapServices()
        {

        }

        void mapOthers()
        {
            injectionBinder.Bind<GameObject>().To(entryPoint.World).ToName(EntryPoint.Container.World);
            injectionBinder.Bind<GameObject>().To(entryPoint.UI).ToName(EntryPoint.Container.UI);
        }
    }
}