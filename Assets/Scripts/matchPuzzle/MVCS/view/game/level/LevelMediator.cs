using System.Collections.Generic;
using UnityEngine;
using matchPuzzle.MVCS.controller.signal;
using matchPuzzle.MVCS.model;
using matchPuzzle.MVCS.model.level;
using matchPuzzle.MVCS.model.level.chain;
using strange.extensions.mediation.impl;

namespace matchPuzzle.MVCS.view.game.level
{
    public class LevelMediator : Mediator
    {
        [Inject]
        public LevelView view
        {
            get;
            set;
        }

        [Inject(GameState.Current)]
        public IChainModel chain
        {
            get;
            set;
        }

        [Inject(GameState.Current)]
        public ILevelModel level
        {
            get;
            set;
        }

        [Inject]
        public IElementsDefModel elements
        {
            get;
            set;
        }

        [Inject]
        public PinElementSignal onPinElement
        {
            get;
            set;
        }

        [Inject]
        public UnpinElementSignal onUnPinElement
        {
            get;
            set;
        }

        [Inject]
        public EliminateElementsSignal onEliminateElements
        {
            get;
            set;
        }

        [Inject]
        public MoveElementsSignal onMoveElements
        {
            get;
            set;
        }

        [Inject]
        public AddElementsSignal onAddElements
        {
            get;
            set;
        }

        static ILevelModel saved;

        public override void OnRegister()
        {
            // view events
            view.OnTryToPin.AddListener(tryToPinHandler);
            view.OnApply.AddListener(tryToApply);

            // model events
            onPinElement.AddListener(pinElementHandler);
            onUnPinElement.AddListener(unpinElementHandler);
            onEliminateElements.AddListener(eliminateElementsHandler);
            onMoveElements.AddListener(moveElementsHandler);
            onAddElements.AddListener(addElementsHandler);

            addStartMap();

            base.OnRegister();
        }

        void tryToPinHandler(Point point)
        {
            if (level.MovesLast <= 0)
                return;

            if (chain.Value.Length != 0 && chain.CanPin(point))
            {
                chain.Pin(point);
            }
        }

        void tryToApply(Point point)
        {
            if (level.MovesLast <= 0)
                return;

            if (chain.Value.Length == 0)
            {
                chain.Pin(point);
            } else {
                if (level.CanEliminate(chain.Value))
                    level.Eliminate(chain.Value);

                chain.Clean();
            }
        }

        void addStartMap()
        {
            view.MapSize = new Point(level.Map.Length, level.Map[0].Length);

            var viewMessages = new List<AddElementWidgetMessage>();

            for (var y = 0; y < level.Map.Length; y++)
            {
                for (var x = 0; x < level.Map[y].Length; x++)
                {
                    var point = new Point(x, y);
                    var elementType = level.Get(point);
                    var viewMessage = new AddElementWidgetMessage(){
                        To = point,
                        Type = elementType,
                        resourceId = elements.GetTextureId(elementType)
                    };

                    viewMessages.Add(viewMessage);
                }
            }

            addElementsHandler(viewMessages.ToArray());
        }

        void pinElementHandler(Point point)
        {
            view.pinElement(point);
        }

        void unpinElementHandler(Point point)
        {
            view.unpinElement(point);
        }

        void eliminateElementsHandler(Point[] eleminatePositions)
        {
            view.EliminateElements(eleminatePositions);
        }

        void moveElementsHandler(MoveElementMessage[] moveMessages)
        {
            view.MoveElements(moveMessages);
        }

        void addElementsHandler(AddElementMessage[] addMessages)
        {
            var viewMessages = new List<AddElementWidgetMessage>();

            foreach (var message in addMessages) {
                var viewMessage = AddElementWidgetMessage.FromBase(message, elements.GetTextureId(message.Type));
                viewMessages.Add(viewMessage);
            }

            view.AddElements(viewMessages.ToArray());
        }

        public override void OnRemove()
        {
            view.OnTryToPin.RemoveListener(tryToPinHandler);
            view.OnApply.RemoveListener(tryToApply);

            onPinElement.RemoveListener(pinElementHandler);
            onUnPinElement.RemoveListener(unpinElementHandler);
            onEliminateElements.RemoveListener(eliminateElementsHandler);
            onMoveElements.RemoveListener(moveElementsHandler);
            onAddElements.RemoveListener(addElementsHandler);

            base.OnRemove();
        }
    }
}