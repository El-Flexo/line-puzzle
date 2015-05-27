using UnityEngine;
using strange.extensions.signal.impl;

namespace matchPuzzle.component
{
    [RequireComponent(typeof(Collider2D))]
    public class MouseTrigger : MonoBehaviour
    {
        public readonly Signal<GameObject> onOver = new Signal<GameObject>();
        public readonly Signal<GameObject> onClick = new Signal<GameObject>();

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