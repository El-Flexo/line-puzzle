using matchPuzzle.MVCS.model.level;

public interface IChain
{
    Point Value {
        get;
    }

    void Pin(Point target);

    bool CanPin(Point target);

    void Clean();
}