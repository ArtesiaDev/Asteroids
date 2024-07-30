using System;
using Develop.Runtime.Core.Configs;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Develop.Runtime.Core.Spawn
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Asteroid : MonoBehaviour
    {
        public event Action AsteroidDied;
        
        [SerializeField] private EnemyConfig _config;
        [SerializeField] private AsteroidTypes _type;

        private AsteroidSpawner _spawner;

        [Inject]
        private void Construct(AsteroidSpawner spawner)
        {
            _spawner = spawner;
        }

        private void OnWeaponShoot()
        {
            switch ((int)_type)
            {
                case 1: case 2: case 3:
                    SplitAsteroid(AsteroidTypes.AsterMed1);
                    DestroyAsteroid();
                    break;

                case 4: case 5: case 6:
                    SplitAsteroid(AsteroidTypes.AsterSmall1);
                    DestroyAsteroid();
                    break;

                case 7: case 8: case 9:
                    DestroyAsteroid();
                    break;

                default: throw new Exception("Unknown type");
            }
        }

        private async void SplitAsteroid (AsteroidTypes fragmentType)
        {
            for (int i = 0; i < _config.FragmentsNumber; i++)
            {
                Asteroid fragment = await _spawner.CreateAsteroid(fragmentType, transform.position);

                Rigidbody2D rb = fragment.GetComponent<Rigidbody2D>();
                
                Vector2 thrust =  Random.insideUnitCircle.normalized * _config.ExplosionForce;
                
                rb.AddForce(thrust, ForceMode2D.Impulse);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Asteroid>() == null)
                OnWeaponShoot();
        }

        private void DestroyAsteroid()
        {
            AsteroidDied?.Invoke();
            Destroy(gameObject);
        }
    }
}