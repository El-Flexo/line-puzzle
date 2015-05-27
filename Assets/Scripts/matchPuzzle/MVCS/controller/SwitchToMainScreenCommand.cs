using UnityEngine;
using matchPuzzle.MVCS.model.level.provider;
using matchPuzzle.MVCS.view.UI.common;
using matchPuzzle.MVCS.view.UI.main;
using matchPuzzle.core.UI;
using strange.extensions.command.impl;

namespace matchPuzzle.MVCS.controller
{
    public class SwitchToMainScreenCommand : Command
    {
        [Inject]
        public UIManager ui {
            get;
            set;
        }

        public override void Execute()
        {
            ui.Show<ScreenMainView>();
        }
    }
}