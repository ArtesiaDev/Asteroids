namespace Develop.Runtime.Meta.EventSignals
{
    public interface ILaserSignalsHandler
    {
        void OnLaserAmmunitionChanged(int currentLaserShots){}
        void OnLaserCooldownChanged(float cooldown){}
    }
}