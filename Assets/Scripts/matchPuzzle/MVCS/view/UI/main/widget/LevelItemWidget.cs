using UnityEngine;
using UnityEngine.UI;
using strange.extensions.signal.impl;
using matchPuzzle.MVCS.model.level.provider;

namespace matchPuzzle.MVCS.view.UI.main.widget
{
    public class LevelItemWidget : MonoBehaviour
    {
        public readonly Signal<int> onSelect = new Signal<int>();

        [SerializeField]
        Button button;

        [SerializeField]
        Text nameLabel;

        [SerializeField]
        Text movesLabel;

        ILevelProvider level;

        int levelIndex;
        
        void Awake()
        {
            button.onClick.AddListener(() => {
                  onSelect.Dispatch(levelIndex);
            });
        }

        public void SetData(ILevelProvider level, int levelIndex)
        {
            nameLabel.text = level.Name;
            movesLabel.text = string.Format("Moves: {0}", level.Moves);
            this.levelIndex = levelIndex;
        }

        void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}