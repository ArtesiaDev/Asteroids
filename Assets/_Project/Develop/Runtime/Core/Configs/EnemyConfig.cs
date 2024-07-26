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
    }
}