using matchPuzzle.MVCS.model.level;

namespace matchPuzzle.MVCS.model.level
{
    public interface IChainModel
    {
        Point[] Value {
            get;
        }

        void Pin(Point target);

        bool CanPin(Point target);

        void Clean();
    }
}