using System;
using matchPuzzle.MVCS.model.level.provider;

namespace matchPuzzle.MVCS.model.level
{
    public class LevelModel : ILevelModel
    {
        public static readonly int MIN_CHAIN_LENGTH = 4;

        [Inject]
        public ILevelProvider provider {
            get;
            set;
        }

        int currentMove = 0;

        [PostConstruct]
        public void Construct()
        {
            Map = provider.InitMap;
        }

        public int[][] Map {
            get;
            private set;
        }

        public int MovesLast {
            get {
                return provider.Moves - currentMove;
            }
        }

        public void Execute(Point[] chain)
        {
            Preconditions.CheckArgument(!CanExecute(chain), string.Format("Required chain length > 4, executable chain length: {0}", chain.Length));

            currentMove++;
            EliminateElements(chain);
            FillMap();
        }

        void EliminateElements(Point[] chain)
        {
            throw new NotImplementedException("Eliminate elements");
        }

        void FillMap()
        {
            throw new NotImplementedException("Fill map");
        }

        public bool CanExecute(Point[] chain)
        {
            return chain.Length >= MIN_CHAIN_LENGTH;
        }
    }
}