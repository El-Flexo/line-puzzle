using matchPuzzle.MVCS.model.level;

public interface ILevelModel
{
    int MovesLast {
        get;
    }

    int Get(Point target);

    void Execute(Point[] chain);

    bool CanExecute(Point[] chain);
}