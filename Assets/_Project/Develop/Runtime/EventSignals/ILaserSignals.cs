using System;

namespace Develop.Runtime.EventSignals
{
    internal interface ILaserSignals
    {
        event Action<int> LaserAmmunitionChanged;
        event Action<float> LaserCooldownChanged;
    }
}