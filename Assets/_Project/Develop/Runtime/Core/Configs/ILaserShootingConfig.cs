namespace Develop.Runtime.Core.Configs
{
    public interface ILaserShootingConfig
    {
        public float LaserCooldown { get; }
        public float LaserOffsetCoefficient { get; }
        public int LaserAmmunition { get; }
        public float LaserLifeTime { get; }
        public float LaserReloadTime { get; }
    }
}