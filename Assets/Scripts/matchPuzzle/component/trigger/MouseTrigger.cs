using UnityEngine;
using strange.extensions.signal.impl;

namespace matchPuzzle.component.trigger
{
    [RequireComponent(typeof(Collider2D))]
    public class MouseTrigger : MonoBehaviour, IInteractionTrigger
    {
        public Signal<GameObject> onOver { get; private set; }
        public Signal<GameObject> onClick { get; private set; }

        void Awake()
        {
            onOver = new Signal<GameObject>();
            onClick = new Signal<GameObject>();
        }

        bool isAlreadyOver = false;

        bool isAlredyDown = false;

        void OnMouseOver()
        {
            if (!isAlreadyOver)
            {
                onOver.Dispatch(gameObject);
                isAlreadyOver = true;
            }
        }

        void OnMouseExit()
        {
            isAlreadyOver = false;
        }

        void OnMouseDown()
        {
            isAlredyDown = true;
        }

        void OnMouseUp()
        {
            if (isAlredyDown && isAlreadyOver)
            {
                onClick.Dispatch(gameObject);
                isAlredyDown = false;
            }
        }
    }
}