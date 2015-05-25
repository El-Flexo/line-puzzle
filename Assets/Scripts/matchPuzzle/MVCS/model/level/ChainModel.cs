using System;
using System.Collections.Generic;
using matchPuzzle.MVCS.controller.signal;

namespace matchPuzzle.MVCS.model.level
{
    public class ChainModel : IChainModel
    {
        static readonly int TAIL_LENGTH = 2;

        [Inject]
        public PinElementSignal pinElement {
            get;
            set;
        }

        [Inject]
        public PinElementSignal unpinElement {
            get;
            set;
        }

        [Inject]
        public ILevelModel level {
            get;
            set;
        }

        List<Point> value = new List<Point>();

        public Point[] Value {
            get {
                return value.ToArray();
            }
        }

        public void Pin(Point target)
        {
            Preconditions.CheckArgument(CanPin(target), string.Format("Invalid target to pin: {0}", target.ToString()));

            if (!isNew(target)) {
                var index = value.IndexOf(target) + 1;
                for (var i = index; i < value.Count; i++)
                    unpinElement.Dispatch(value[i]);

                value = value.GetRange(0, index);
            } else {
                value.Add(target);
                pinElement.Dispatch(target);
            }
        }

        public bool CanPin(Point target)
        {
            if (value.Count == 0)
                return true;

            if (isNew(target))
                return isSameType(target) && isNeighbour(target);
            else
                return isNearToEnd(target);
        }

        public bool isNeighbour(Point target)
        {
            var last = value[value.Count - 1];
            return Math.Abs(last.x - target.x) <= 1 && Math.Abs(last.y - target.y) <= 1;
        }

        bool isSameType(Point target)
        {
            return level.Get(value[0]) == level.Get(target);
        }

        public bool isNew(Point target)
        {
            return value.IndexOf(target) == - 1;
        }

        bool isNearToEnd(Point target)
        {
            var tailLength = value.Count - value.IndexOf(target) - 1;
            return tailLength <= TAIL_LENGTH;
        }

        public void Clean()
        {
            foreach (var point in value)
                unpinElement.Dispatch(point);
            value = new List<Point>();
        }
    }
}