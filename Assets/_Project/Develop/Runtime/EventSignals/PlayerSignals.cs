using System;

namespace Develop.Runtime.EventSignals
{
    public class PlayerSignals : IPlayerSignals, IPlayerSignalsHandler
    {
        public event Action PlayerDied;
        public event Action PlayerMoved;
        public event Action PlayerSteered;

        public void OnPlayerDied() =>
            PlayerDied?.Invoke();

        public void OnPlayerMoved() =>
            PlayerMoved?.Invoke();

        public void OnPlayerSteered() =>
            PlayerSteered?.Invoke();
    }
}