using UnityEngine;
using matchPuzzle.MVCS.model;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace matchPuzzle.MVCS.view.game.level
{
    public class LevelView : View
    {
        [SerializeField]
        GameObject elementsContainer;

        [SerializeField]
        GameObject elementPrefab;

        public readonly Signal<Point> OnTryToPin = new Signal<Point>();
        public readonly Signal OnApply = new Signal();
        public readonly Signal OnMissClick = new Signal();
    }
}