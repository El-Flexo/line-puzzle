using strange.extensions.signal.impl;

namespace matchPuzzle.MVCS.controller.signal
{
    public class StratupSignal : Signal
    {
    }

    public class StartLevelSignal : Signal<int>
    {
    }

    public class SwitchToMainScreenSignal : Signal
    {
    }

    public class RetyLevelSignal : Signal<int>
    {
    }
}