using UnityEngine;
using matchPuzzle.MVCS.model;
using matchPuzzle.component.trigger;

namespace matchPuzzle.MVCS.view.game.level
{
    [RequireComponent(typeof(Sprite))]
    public class ElementWidget : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer selector;

        SpriteRenderer elementImage;

        public IInteractionTrigger trigger {
            get;
            private set;
        }

        public Point Position;

        void Awake()
        {
            elementImage = gameObject.GetComponent<SpriteRenderer>();
            trigger = gameObject.GetComponent<IInteractionTrigger>();
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