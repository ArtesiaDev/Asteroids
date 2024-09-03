namespace Develop.Runtime.Services.Input
{
    public class MobileInput: Input
    {
        private const string UP_BUTTON = "UpButton";
        private const string BULLET_BUTTON = "BulletButton";
        private const string LASER_BUTTON = "LaserButton";

        public override bool Move =>
            SimpleInput.GetButton(UP_BUTTON);

        public override bool BulletShoot =>
            SimpleInput.GetButton(BULLET_BUTTON);

        public override bool LaserShoot =>
            SimpleInput.GetButton(LASER_BUTTON);
    }
}