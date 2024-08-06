using R3;

namespace Develop.Runtime.EventSignals
{
    public class LaserSignals : ILaserSignals, ILaserSignalsHandler
    {
        public Observable<int> LaserAmmunition => _laserAmmunition; 
        private readonly ReactiveProperty<int> _laserAmmunition  = new ReactiveProperty<int>();

        public Observable<float> LaserCooldown => _laserCooldown;
        private readonly ReactiveProperty<float> _laserCooldown = new ReactiveProperty<float>();

        public void OnLaserAmmunitionChanged(int currentLaserShots) =>
            _laserAmmunition.Value = currentLaserShots;

        public void OnLaserCooldownChanged(float cooldown) =>
            _laserCooldown.Value = cooldown;
    }
}