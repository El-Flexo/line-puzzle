using matchPuzzle.MVCS.model.level.provider;

namespace matchPuzzle.MVCS.model.level
{
    public interface ILevelListModel
    {
        ILevelProvider[] LevelsAvailable {
            get;
        }
    }
}