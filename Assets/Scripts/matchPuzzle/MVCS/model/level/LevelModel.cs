using System;
using System.Collections.Generic;
using matchPuzzle.MVCS.controller.signal;
using matchPuzzle.MVCS.model.level.chain;
using matchPuzzle.MVCS.model.level.provider;
using strange.extensions.signal.impl;

namespace matchPuzzle.MVCS.model.level
{
    public class LevelModel : ILevelModel
    {
        public static readonly int MIN_CHAIN_LENGTH = 4;

        [Inject]
        public ILevelProvider provider {
            get;
            set;
        }

        [Inject]
        public IElementGenerator generator {
            get;
            set;
        }

        [Inject]
        public EliminateElementsSignal eliminateElements {
            get;
            set;
        }

        [Inject]
        public MoveElementsSignal moveElements {
            get;
            set;
        }

        [Inject]
        public AddElementsSignal addElements {
            get;
            set;
        }

        int currentMove = 0;

        [PostConstruct]
        public void Construct()
        {
            Map = provider.InitMap;
        }

        public int[][] Map {
            get;
            private set;
        }

        public int MovesLast {
            get {
                return provider.Moves - currentMove;
            }
        }

        public void Eliminate(Point[] chain)
        {
            Preconditions.CheckArgument(CanEliminate(chain), string.Format("Required chain length > 4, executable chain length: {0}", chain.Length));

            currentMove++;
            EliminateElements(chain);
            SquashMap();
            FillMap();
        }

        public void EliminateElements(Point[] chain)
        {
            foreach (var target in chain)
                Map[target.y][target.x] = (int)ElementType.Empty;
            eliminateElements.Dispatch(chain);
        }

        public void SquashMap()
        {
            var elementsToMove = new List<MoveElementMessage>();

            var height = Map.Length;
            var width = Map[0].Length;
            for (var x = 0; x < width; x++) {
                var movePoint = height - 1;
                for (var y = height - 1; y >= 0; y--) {
                    var item = Map[y][x];
                    var isExist = item != (int) ElementType.Empty;

                    if (isExist) {
                        if (y != movePoint)
                        {
                            elementsToMove.Add(new MoveElementMessage(){
                                From = new Point(x, y),
                                To = new Point(x, movePoint)
                            });

                            Map[movePoint][x] = Map[y][x];
                            Map[y][x] = (int) ElementType.Empty;
                            movePoint = movePoint - 1;
                        } else
                            movePoint = y - 1;
                    }
                }
            }
            if (elementsToMove.Count > 0)
                moveElements.Dispatch(elementsToMove.ToArray());
        }

        void FillMap()
        {
            var elemtsToAdd = new List<AddElementMessage>();
            for (var y = 0; y < Map.Length; y++)
            {
                for (var x = 0; x < Map[y].Length; x++) {
                    var isEmpty = Map[y][x] == (int)ElementType.Empty;
                    if (isEmpty)
                    {
                        var elementType = generator.GetNext();
                        Map[y][x] = (int)elementType;
                        elemtsToAdd.Add(new AddElementMessage(){
                            Type = elementType,
                            To = new Point(x, y)
                        });
                    }
                }
            }
            if (elemtsToAdd.Count > 0)
                addElements.Dispatch(elemtsToAdd.ToArray());
        }

        public bool CanEliminate(Point[] chain)
        {
            return chain.Length >= MIN_CHAIN_LENGTH;
        }

        public ElementType Get(Point target)
        {
            return (ElementType) Map[target.y][target.x];
        }
    }

    public struct MoveElementMessage
    {
        public Point From;
        public Point To;
    }

    public struct AddElementMessage
    {
        public Point To;
        public ElementType Type;
    }
}