using System;
using R3;

namespace Develop.Runtime.EventSignals
{
    internal interface ILaserSignals
    {
        Observable<int> LaserAmmunition { get; }
        Observable<float> LaserCooldown { get; }
    }
}