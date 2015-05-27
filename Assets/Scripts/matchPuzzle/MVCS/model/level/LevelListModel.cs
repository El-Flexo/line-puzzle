using System.Collections.Generic;
using UnityEngine;
using matchPuzzle.MVCS.model.level.provider;

namespace matchPuzzle.MVCS.model.level
{
    public class LevelListModel : ILevelListModel
    {
        List<ILevelProvider> availableLevels = new List<ILevelProvider>();

        public ILevelProvider[] LevelsAvailable {
            get {
                return availableLevels.ToArray();
            }
        }

        [PostConstruct]
        public void Construct()
        {
            var levels = Resources.LoadAll<TextAsset>("defs/levels");
            foreach (var levelSource in levels)
            {
                var level = new DefLevelProvider();
                level.SetDef(levelSource.text);
                availableLevels.Add(level);
            }
        }
    }

    public interface ILevelListModel
    {
        ILevelProvider[] LevelsAvailable {
            get;
        }
    }
}