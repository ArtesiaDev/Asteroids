using System;

namespace Develop.Runtime.Meta.EventSignals
{
    public interface IPlayerSignals
    {
        event Action PlayerSteered;
        event Action PlayerDied;
        event Action PlayerMoved;
    }
}