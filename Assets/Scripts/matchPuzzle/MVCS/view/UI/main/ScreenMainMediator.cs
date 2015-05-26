using matchPuzzle.MVCS.model.level;
using strange.extensions.mediation.impl;
using matchPuzzle.MVCS.controller.signal;

namespace matchPuzzle.MVCS.view.UI.main
{
    public class ScreenMainMediator : Mediator
    {
        [Inject]
        public ILevelListModel levels
        {
            get;
            set;
        }

        [Inject]
        public ScreenMainView view
        {
            get;
            set;
        }

        [Inject]
        public StartLevelSignal startLevel
        {
            get;
            set;
        }

        public override void OnRegister()
        {
            view.SetLevels(levels.LevelsAvailable);
            view.onItemSelect.AddListener(SelectLevel);

            base.OnRegister();
        }

        void SelectLevel(int levelIndex)
        {
            startLevel.Dispatch(levelIndex);
            Destroy(this.gameObject);
        }

        public override void OnRemove()
        {
            view.onItemSelect.RemoveListener(SelectLevel);
            base.OnRemove();
        }
    }
}