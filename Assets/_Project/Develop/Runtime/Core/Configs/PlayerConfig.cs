using UnityEngine;

namespace Develop.Runtime.Core.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [field: Header("Steering System")]
        [field: SerializeField]
        public float RotationSpeed { get; private set; }

        [field: Header("Mover System")]
        [field: SerializeField]
        public float ThrustPower { get; private set; }

        [field: SerializeField] public float MaxSpeed { get; private set; }

        [field: Header("BulletShooting")]
        [field: SerializeField]
        public float FireRate { get; private set; }

        [field: SerializeField, Range(0.8f, 10f)]
        public float BulletOffsetCoefficient { get; private set; }

        [field: SerializeField] public float Speed { get; private set; }

        [field: Header("LaserShooting")]
        [field: SerializeField]
        public float Cooldown { get; private set; }

        [field: SerializeField, Range(0.8f, 10f)]
        public float LaserOffsetCoefficient { get; private set; }
        [field: SerializeField] public int Ammunition { get; private set; }
        
        [field: SerializeField] public float LaserLifeTime { get; private set; }
        [field: SerializeField] public float ReloadTime { get; private set; }
    }
}