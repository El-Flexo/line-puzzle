using System;
using System.Collections.Generic;
using UnityEngine;
using matchPuzzle.MVCS.view.UI.HUD;
using matchPuzzle.MVCS.view.UI.common;
using matchPuzzle.MVCS.view.UI.main;

namespace matchPuzzle.core.UI
{
    public class UIMap
    {
        static readonly Dictionary<Type, string> map = new Dictionary<Type, string>(){
            {typeof(ScreenMainView), "UI/ScreenMain"},
            {typeof(ScreenHUDView), "UI/ScreenHUD"},
            {typeof(DialogCommonView), "UI/DialogCommon"}
        };

        public string GetPath(Type uiType)
        {
            string prefabPath;
            if (!map.TryGetValue(uiType, out prefabPath))
                throw new ArgumentException(string.Format("Undefined prefab path for UI type: {0}", uiType));
            return prefabPath;
        }
    }
}