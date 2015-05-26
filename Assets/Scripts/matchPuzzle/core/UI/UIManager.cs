using UnityEngine;
using matchPuzzle.utils;

namespace matchPuzzle.core.UI
{
    public class UIManager
    {
        [Inject(EntryPoint.Container.UI)]
        public GameObject uiContainer
        {
            get;
            set;
        }

        [Inject]
        public UIMap uiMap
        {
            get;
            set;
        }

        public TView Show<TView>() where TView: MonoBehaviour
        {
            var uiPath = uiMap.GetPath(typeof(TView));
            var prefab = (GameObject)Resources.Load(uiPath);

            var instance = InstantiateUtil.InstantiateUIAt(prefab, uiContainer);
            return instance.GetComponent<TView>();
        }
    }
}