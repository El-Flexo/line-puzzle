using UnityEngine;
using matchPuzzle.MVCS.model;
using matchPuzzle.component;
using strange.extensions.signal.impl;

namespace matchPuzzle.MVCS.view.game.level
{
    [RequireComponent(typeof(Sprite))]
    [RequireComponent(typeof(MouseTrigger))]
    public class ElementWidget : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer selector;

        SpriteRenderer elementImage;

        public MouseTrigger trigger {
            get;
            private set;
        }

        public Point Position;

        void Awake()
        {
            elementImage = gameObject.GetComponent<SpriteRenderer>();
            trigger = gameObject.GetComponent<MouseTrigger>();
        }

        public void Init(Point position, string textureId)
        {
            SetTexture(textureId);
            Position = position;
        }

        void SetTexture(string resourceId)
        {
            var sprite = Resources.Load<Sprite>(resourceId);
            elementImage.sprite = sprite;
        }

        public void Select()
        {
            selector.enabled = true;
        }

        public void Unselect()
        {
            selector.enabled = false;
        }
    }
}