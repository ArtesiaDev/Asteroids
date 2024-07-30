using Develop.Runtime.Core.Spawn;
using UnityEngine;

namespace Develop.Runtime.Core.ShootingObjects
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Laser: MonoBehaviour
    {
        public void Destroy(float time) =>
            Destroy(gameObject, time);
    }
}