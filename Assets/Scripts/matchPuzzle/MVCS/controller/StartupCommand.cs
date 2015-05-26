using matchPuzzle.MVCS.view.UI.main;
using strange.extensions.command.impl;
using matchPuzzle.core.UI;

namespace matchPuzzle.MVCS.controller
{
    public class StartupCommand : Command
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