using UnityEngine;
using UnityEngine.UI;
using matchPuzzle.component;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace matchPuzzle.MVCS.view.UI.HUD
{
    public class ScreenHUDView : View
    {
        [SerializeField]
        Button endButton;

        [SerializeField]
        Button retyButton;

        [SerializeField]
        Button infoButton;

        [SerializeField]
        Text score;

        [SerializeField]
        Text moves;

        [SerializeField]
        ProgressBar progressbar;

        public readonly Signal onExitLevel = new Signal();
        public readonly Signal onRetyLevel = new Signal();
        public readonly Signal onInfoRequired = new Signal();

        protected override void Awake()
        {
            endButton.onClick.AddListener(onExitLevel.Dispatch);
            retyButton.onClick.AddListener(onRetyLevel.Dispatch);
            infoButton.onClick.AddListener(onInfoRequired.Dispatch);

            base.Awake();
        }

        public void SetScoreProgress(int score, int requiredScore)
        {
            this.score.text = score.ToString();
            progressbar.SetProgress((float)score / (float)requiredScore);
        }

        public void SetMovesLast(int moves)
        {
            this.moves.text = moves.ToString();
        }

        protected override void OnDestroy()
        {
            endButton.onClick.RemoveListener(onExitLevel.Dispatch);
            retyButton.onClick.RemoveListener(onRetyLevel.Dispatch);
            infoButton.onClick.RemoveListener(onInfoRequired.Dispatch);

            base.OnDestroy();
        }

    }
}