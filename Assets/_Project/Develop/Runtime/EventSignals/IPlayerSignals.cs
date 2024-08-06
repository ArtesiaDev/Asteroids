using System;

namespace Develop.Runtime.EventSignals
{
    public interface IPlayerSignals
    {
        event Action PlayerSteered;
        event Action PlayerDied;
        event Action PlayerMoved;
    }
}