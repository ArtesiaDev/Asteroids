namespace Develop.Runtime.Core.Configs
{
    public interface ILaserShootingConfig
    {
        public float Cooldown { get; }
        public float LaserOffsetCoefficient { get; }
        public int Ammunition { get; }
        public float LaserLifeTime { get; }
        public float ReloadTime { get; }
    }
}