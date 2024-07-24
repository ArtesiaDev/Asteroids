using UnityEngine;

namespace Develop.Runtime.Core.ShootingObjects
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Laser: MonoBehaviour
    {
        public void Destroy(float time) =>
            Destroy(gameObject, time);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(collision.gameObject);
        }
    }
}