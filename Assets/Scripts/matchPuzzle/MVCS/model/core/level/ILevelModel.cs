using matchPuzzle.MVCS.model.level;

namespace matchPuzzle.MVCS.model.level
{
    public interface ILevelModel
    {
        int MovesLast {
            get;
        }

        int Score {
            get;
        }

        int RequiredScore {
            get;
        }

        int[][] Map {
            get;
        }

        void Eliminate(Point[] chain);

        bool CanEliminate(Point[] chain);

        ElementType Get(Point target);
    }
}