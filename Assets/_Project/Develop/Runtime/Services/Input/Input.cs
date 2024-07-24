namespace Develop.Runtime.Services.Input
{
    public abstract class Input: IInput
    {
        public virtual bool Move { get; }
        
        public virtual float Steer { get; }
        
        public virtual bool BulletShoot { get; }
        
        public virtual bool LaserShoot { get; }
    }
}