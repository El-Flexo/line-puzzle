using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using matchPuzzle.MVCS.model;
using matchPuzzle.MVCS.model.level;
using matchPuzzle.utils;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

namespace matchPuzzle.MVCS.view.game.level
{
    public class LevelView : View
    {
        static readonly float cellSize = 1.6f;

        [SerializeField]
        Camera gameCamera;

        [SerializeField]
        GameObject elementsContainer;

        [SerializeField]
        GameObject elementPrefab;

        public readonly Signal<Point> OnTryToPin = new Signal<Point>();
        public readonly Signal<Point> OnApply = new Signal<Point>();
        public Point MapSize;

        readonly List<ElementWidget> elements = new List<ElementWidget>();

        void CreateElement(Point position, string textureId)
        {
            var instance = InstantiateUtil.InstantiateAt(elementPrefab, elementsContainer); // TODO: если использовать пул, то здесь нужно брать объект из него
            var element = instance.GetComponent<ElementWidget>();
            element.Init(position, textureId);
            element.transform.position = WorldFromCellPosition(position);
            element.trigger.onOver.AddListener(ItemSelectedHandler);
            element.trigger.onClick.AddListener(ItemClickHandler);

            elements.Add(element);
        }

        void ItemSelectedHandler(GameObject widgetGO)
        {
            var element = widgetGO.GetComponent<ElementWidget>();
            OnTryToPin.Dispatch(element.Position);
        }

        void ItemClickHandler(GameObject widgetGO)
        {
            var element = widgetGO.GetComponent<ElementWidget>();
            OnApply.Dispatch(element.Position);
        }

        void unmapEvents(ElementWidget element)
        {
            element.trigger.onOver.RemoveListener(ItemSelectedHandler);
            element.trigger.onClick.RemoveListener(ItemClickHandler);
        }

        public void pinElement(Point position)
        {
            var element = elements.Find((e) => e.Position.Equals(position));
            element.Select();
        }

        public void unpinElement(Point position)
        {
            var element = elements.Find((e) => e.Position.Equals(position));
            element.Unselect();
        }

        public void AddElements(AddElementWidgetMessage[] addMessages)
        {
            foreach (var message in addMessages) {
                CreateElement(message.To, message.resourceId);
            }
        }

        public void MoveElements(MoveElementMessage[] moveMessages)
        {
            foreach (var message in moveMessages) {
                var element = elements.Find((e) => e.Position.Equals(message.From));
                element.gameObject.transform.position = WorldFromCellPosition(message.To);
                element.Position = message.To;
            }
        }

        public void EliminateElements(Point[] eliminatePositions)
        {
            foreach (var position in eliminatePositions) {
                var element = elements.Find((e) => e.Position.Equals(position));
                unmapEvents(element);
                elements.Remove(element);
                Destroy(element.gameObject); // TODO: если использовать пул, то здесь надо освобождать объект
            }
        }

        protected override void OnDestroy()
        {
            foreach (var widget in elements)
                unmapEvents(widget);
            base.OnDestroy();
        }

        public Vector3 WorldFromCellPosition(Point source)
        {
            return new Vector3((MapSize.x - source.x) * cellSize, (MapSize.y - source.y) * cellSize);
        }
    }

    public class AddElementWidgetMessage : AddElementMessage
    {
        public string resourceId;

        public static AddElementWidgetMessage FromBase(AddElementMessage baseMessage, string resourceId)
        {
            return new AddElementWidgetMessage(){
                To = baseMessage.To,
                Type = baseMessage.Type,
                resourceId = resourceId
            };
        }
    }
}