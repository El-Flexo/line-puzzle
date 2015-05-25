using matchPuzzle.MVCS.model;
using matchPuzzle.MVCS.model.level;
using strange.extensions.signal.impl;

namespace matchPuzzle.MVCS.controller.signal
{
    public class EliminateElementsSignal : Signal<Point[]>
    {
    }

    public class MoveElementsSignal : Signal<MoveElementMessage[]>
    {
    }

    public class AddElementsSignal : Signal<AddElementMessage[]>
    {
    }
}