using matchPuzzle.MVCS.model;
using matchPuzzle.MVCS.model.level;
using matchPuzzle.MVCS.model.level.chain;
using strange.extensions.mediation.impl;

namespace matchPuzzle.MVCS.view.game.level
{
    public class LevelMediator : Mediator
    {
        [Inject]
        public LevelView view {
            get;
            set;
        }

        [Inject(GameState.Current)]
        public IChainModel chain {
            get;
            set;
        }

        [Inject(GameState.Current)]
        public ILevelModel level {
            get;
            set;
        }

        public override void OnRegister()
        {
            addStartMap();

            base.OnRegister();
        }

        void addStartMap()
        {

        }
    }
}