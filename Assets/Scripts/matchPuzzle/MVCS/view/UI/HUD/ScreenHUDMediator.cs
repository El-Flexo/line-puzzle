using matchPuzzle.MVCS.controller.signal;
using matchPuzzle.MVCS.model;
using matchPuzzle.MVCS.model.level;
using matchPuzzle.MVCS.view.UI.HUD;
using matchPuzzle.MVCS.view.UI.common;
using matchPuzzle.core.UI;
using strange.extensions.mediation.impl;

namespace matchPuzzle.MVCS.view.UI.main
{
    public class ScreenHUDMediator : Mediator
    {
        [Inject]
        public ScreenHUDView view
        {
            get;
            set;
        }

        [Inject(GameState.Current)]
        public ILevelModel level
        {
            get;
            set;
        }

        [Inject]
        public EliminateElementsSignal onEliminateElements
        {
            get;
            set;
        }

        [Inject]
        public SwitchToMainScreenSignal toMainScreenSignal
        {
            get;
            set;
        }

        [Inject]
        public RetyLevelSignal retyLevelSignal
        {
            get;
            set;
        }

        [Inject]
        public UIManager ui {
            get;
            set;
        }

        [Inject(GameState.Current)]
        public int levelIndex {
            get;
            set;
        }

        public override void OnRegister()
        {
            view.onExitLevel.AddListener(exitLevelHandler);
            view.onRetyLevel.AddListener(retyLevelHandler);
            view.onInfoRequired.AddListener(infoRequiredHandler);

            onEliminateElements.AddListener(eliminateElementsHandler);
            updateLevelProgress();

            base.OnRegister();
        }

        void updateLevelProgress()
        {
            view.SetMovesLast(level.MovesLast);
            view.SetScoreProgress(level.Score, level.RequiredScore);
        }

        void eliminateElementsHandler(Point[] points)
        {
            updateLevelProgress();
        }

        void exitLevelHandler()
        {
            var dialog = ui.Show<DialogCommonView>();
            dialog.InitYesNo(
            "Exit level",
            "Your score will be lost.\nAre you shure,\nmy young padawan?",
            "Okay",
            toMainScreenSignal.Dispatch,
            "Cancel");
        }

        void retyLevelHandler()
        {
            var dialog = ui.Show<DialogCommonView>();
            dialog.InitYesNo(
            "Rety level",
            "Your score will be lost,\nmy young padawan.",
            "Rety",
            () => retyLevelSignal.Dispatch(levelIndex),
            "Cancel");
        }

        void infoRequiredHandler()
        {
            var dialog = ui.Show<DialogCommonView>();
            dialog.InitOk("Info", "Click to item start select/deselect chain\n" +
            "Move mouse for select elements.\n" +
            "Think what you do!\n" +
            "Maybe, Darth Vader is your father.", "Okay", null);
        }

        public override void OnRemove()
        {
            view.onExitLevel.RemoveListener(exitLevelHandler);
            view.onRetyLevel.RemoveListener(retyLevelHandler);
            view.onInfoRequired.RemoveListener(infoRequiredHandler);
            onEliminateElements.RemoveListener(eliminateElementsHandler);

            base.OnRemove();
        }
    }
}