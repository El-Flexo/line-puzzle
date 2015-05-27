using UnityEngine;
using matchPuzzle.MVCS.model.level.provider;
using matchPuzzle.MVCS.view.UI.common;
using matchPuzzle.MVCS.view.UI.main;
using matchPuzzle.core.UI;
using strange.extensions.command.impl;

namespace matchPuzzle.MVCS.controller
{
    public class CleanGameContainersCommand : Command
    {
        [Inject(EntryPoint.Container.World)]
        public GameObject world {
            get;
            set;
        }

        [Inject(EntryPoint.Container.UI)]
        public GameObject UI {
            get;
            set;
        }

        public override void Execute()
        {
            cleanContainer(world);
            cleanContainer(UI);
        }

        void cleanContainer(GameObject container)
        {
            foreach (Transform child in container.transform) {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}