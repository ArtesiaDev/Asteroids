using System;

namespace Develop.Runtime.EventSignals
{
    public class LaserSignals: ILaserSignals, ILaserSignalsHandler
    {
        public event Action<int> LaserAmmunitionChanged;
        public event Action<float> LaserCooldownChanged;

        public void OnLaserAmmunitionChanged(int currentLaserShots) =>
            LaserAmmunitionChanged?.Invoke(currentLaserShots);

        public void OnLaserCooldownChanged(float cooldown) =>
            LaserCooldownChanged?.Invoke(cooldown);
    }
}