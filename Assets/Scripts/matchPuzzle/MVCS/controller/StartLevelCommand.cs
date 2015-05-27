using UnityEngine;
using matchPuzzle.MVCS.model;
using matchPuzzle.MVCS.model.level;
using matchPuzzle.MVCS.model.level.chain;
using matchPuzzle.MVCS.model.level.provider;
using matchPuzzle.MVCS.view.UI.HUD;
using matchPuzzle.core.UI;
using matchPuzzle.utils;
using strange.extensions.command.impl;

namespace matchPuzzle.MVCS.controller
{
    public class StartLevelCommand : Command
    {
        [Inject(EntryPoint.Container.World)]
        public GameObject world {
            get;
            set;
        }

        [Inject]
        public int levelIndex {
            get;
            set;
        }

        [Inject]
        public ILevelListModel levelList {
            get;
            set;
        }

        [Inject]
        public UIManager ui {
            get;
            set;
        }

        public override void Execute()
        {
            safeUnbind<ILevelProvider>();
            safeUnbind<ILevelModel>(GameState.Current);
            safeUnbind<IChainModel>(GameState.Current);
            safeUnbind<int>(GameState.Current);

            var provider = levelList.LevelsAvailable[levelIndex];
            injectionBinder.Bind<ILevelProvider>().To(provider);

            var level = (GameObject) Resources.Load(provider.View);
            InstantiateUtil.InstantiateAt(level, world);

            var chain = injectionBinder.GetInstance<IChainModel>() as ChainModel;

            injectionBinder.Bind<ILevelModel>().To(chain.level).ToName(GameState.Current);
            injectionBinder.Bind<IChainModel>().To(chain).ToName(GameState.Current);
            injectionBinder.Bind<int>().To(levelIndex).ToName(GameState.Current);

            ui.Show<ScreenHUDView>();
        }

        void safeUnbind<T>()
        {
            var binding = injectionBinder.GetBinding<T>();
            if (binding != null)
                injectionBinder.Unbind<T>();
        }

        void safeUnbind<T>(object name)
        {
            var binding = injectionBinder.GetBinding<T>(name);
            if (binding != null)
                injectionBinder.Unbind<T>(name);
        }
    }
}