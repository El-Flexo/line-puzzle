namespace matchPuzzle.MVCS.model.level.provider
{
    public interface ILevelProvider
    {
        int[][] InitMap {
            get;
        }

        int Moves {
            get;
        }
        
        string Name {
            get;
        }
    }
}