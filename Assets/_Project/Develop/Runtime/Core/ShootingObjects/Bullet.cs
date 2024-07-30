using Develop.Runtime.Core.Spawn;
using UnityEngine;

namespace Develop.Runtime.Core.ShootingObjects
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rb;

        public void Shoot( Vector2 thrust, ForceMode2D forceMode) =>
            _rb.AddForce(thrust, forceMode);

        private void Awake() => 
            _rb = GetComponent<Rigidbody2D>();

        private void OnCollisionEnter2D(Collision2D other)
        {
            // if (collision.gameObject.TryGetComponent(out Asteroid asteroid))
            //     asteroid.OnWeaponShoot();
            Destroy(gameObject);
        }
        
        private void OnBecameInvisible() =>
            Destroy(gameObject);
    }
}