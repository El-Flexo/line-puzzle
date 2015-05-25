using UnityEngine;
using matchPuzzle.MVCS.model;
using strange.extensions.signal.impl;

namespace matchPuzzle.MVCS.view.game.level
{
    public class ElementWidget : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer selector;

        public readonly Signal<ElementWidget> OnInputSelected = new Signal<ElementWidget>();

        bool signalSent = false;

        void OnMouseOver()
        {
            OnInputSelected.Dispatch(this);
            signalSent = true;
        }

        void OnMouseExit()
        {
            signalSent = false;
        }

        public void Select()
        {
            selector.enabled = false;
        }

        public void Unselect()
        {
            selector.enabled = true;
        }
    }
}