using System;

namespace Develop.Runtime.Meta.EventSignals
{
    internal interface ILaserSignals
    {
        event Action<int> LaserAmmunitionChanged;
        event Action<float> LaserCooldownChanged;
    }
}