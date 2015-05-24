using matchPuzzle.MVCS.model.level;

namespace matchPuzzle.MVCS.model.level
{
    public interface ILevelModel
    {
        int MovesLast {
            get;
        }

        int[][] Map {
            get;
        }

        void Execute(Point[] chain);

        bool CanExecute(Point[] chain);
    }
}