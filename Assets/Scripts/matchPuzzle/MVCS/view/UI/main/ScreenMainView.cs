using UnityEngine;
using strange.extensions.mediation.impl;
using matchPuzzle.MVCS.model.level.provider;
using matchPuzzle.utils;
using matchPuzzle.MVCS.view.UI.main.widget;
using strange.extensions.signal.impl;

namespace matchPuzzle.MVCS.view.UI.main
{
    public class ScreenMainView : View
    {
        [SerializeField]
        GameObject levelItemPrefab;

        [SerializeField]
        GameObject levelList;

        public readonly Signal<int> onItemSelect = new Signal<int>();

        public void SetLevels(ILevelProvider[] levels)
        {
            for (var i = 0; i < levels.Length; i++)
            {
                var level = levels[i];
                createLevelWidget(level, i);
            }
        }

        void createLevelWidget(ILevelProvider level, int levelIndex)
        {
            var instance = InstantiateUtil.InstantiateUIAt(levelItemPrefab, levelList);
            var levelWidget = instance.GetComponent<LevelItemWidget>();
            levelWidget.SetData(level, levelIndex);
            levelWidget.onSelect.AddListener(onItemSelect.Dispatch);
        }

        protected override void OnDestroy()
        {
            var items = levelList.GetComponentsInChildren<LevelItemWidget>();
            foreach (var item in items)
                item.onSelect.RemoveListener(onItemSelect.Dispatch);

            base.OnDestroy();
        }
    }
}