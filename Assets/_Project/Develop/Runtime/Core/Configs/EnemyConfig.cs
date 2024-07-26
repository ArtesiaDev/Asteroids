using UnityEngine;

namespace Develop.Runtime.Core.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig", order = 0)]
    public class EnemyConfig : ScriptableObject
    {
        [field: Header("Asteroid")]
        [field: SerializeField]
        public float SpawnInterval { get; private set; }
        
        [field: SerializeField] 
        public float HugeSpawnRadius { get; private set; }
        
        [field: SerializeField] 
        public float MedSpawnRadius { get; private set; }
        
        [field: SerializeField] 
        public float SmallSpawnRadius { get; private set; }

        [field: SerializeField] 
        public int FragmentsNumber { get; private set; }

        [field: SerializeField] 
        public float ExplosionForce { get; private set; }

        [field: SerializeField, Range(0f, 100f)]
        public int SpawnBorderX { get; private set; }

        [field: SerializeField, Range(0f, 100f)]
        public int SpawnBorderY { get; private set; }
    }
}