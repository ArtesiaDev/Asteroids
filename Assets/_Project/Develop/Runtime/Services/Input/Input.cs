namespace Develop.Runtime.Services.Input
{
    public abstract class Input: IInput
    {
        private const string HORIZONTAL = "Horizontal";

        public float Steer =>
            SimpleInput.GetAxis(HORIZONTAL);

        public abstract bool Move { get; }

        public abstract bool BulletShoot { get; }
        
        public abstract bool LaserShoot { get; }
    }
}