using UnityEngine;
using matchPuzzle.MVCS.view.UI.common;
using matchPuzzle.MVCS.view.UI.main;
using matchPuzzle.core.UI;
using strange.extensions.command.impl;

namespace matchPuzzle.MVCS.controller
{
    public class LevelCompleteCommand : Command
    {
        [Inject]
        public UIManager ui {
            get;
            set;
        }

        public override void Execute()
        {
            Retain();
            var dialog = ui.Show<DialogCommonView>();
            dialog.InitOk("You win", "Congrats, honey,\nyou conquest that level.", "GLHF", Release);
        }
    }
}