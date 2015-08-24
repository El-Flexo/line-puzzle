using strange.extensions.signal.impl;
using UnityEngine;

namespace matchPuzzle.component.trigger
{
    public interface IInteractionTrigger
    {
        Signal<GameObject> onOver { get; }
        Signal<GameObject> onClick { get; }
    }
}