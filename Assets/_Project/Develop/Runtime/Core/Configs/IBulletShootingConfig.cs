namespace Develop.Runtime.Core.Configs
{
    public interface IBulletShootingConfig
    {
        public float FireRate { get; }
        public float BulletOffsetCoefficient { get; }
        public float Speed { get; }
    }
}