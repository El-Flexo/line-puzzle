using UnityEngine;
using matchPuzzle.MVCS;
using strange.extensions.context.impl;

namespace matchPuzzle
{
    public class EntryPoint : ContextView
    {
        [SerializeField] GameObject world;
        [SerializeField] GameObject ui;

        void Start()
        {
            context = new AppContext(this);
            context.Start();
        }

        public GameObject World {
            get {
                return world;
            }
        }

        public GameObject UI {
            get {
                return ui;
            }
        }

        public enum Container
        {
            World,
            UI
        }
    }
}