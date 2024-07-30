using System;

namespace Develop.Runtime.Meta.EventSignals
{
    public class AsteroidSignals : IAsteroidSignals, IAsteroidSignalsHandler
    {
        public event Action AsteroidDied;
        
        public void OnAsteroidDied() =>
            AsteroidDied?.Invoke();
    }
}