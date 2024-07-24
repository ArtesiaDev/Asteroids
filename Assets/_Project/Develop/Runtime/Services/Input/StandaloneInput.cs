using UnityEngine;

namespace Develop.Runtime.Services.Input
{
    public class StandaloneInput : Input
    {
        private const string HORIZONTAL = "Horizontal";

        public override bool Move =>
            UnityEngine.Input.GetKey(KeyCode.UpArrow) || UnityEngine.Input.GetKey(KeyCode.W);

        public override float Steer =>
            UnityEngine.Input.GetAxis(HORIZONTAL);

        public override bool BulletShoot =>
            UnityEngine.Input.GetKey(KeyCode.E);

        public override bool LaserShoot =>
            UnityEngine.Input.GetKey(KeyCode.Q);
    }
}