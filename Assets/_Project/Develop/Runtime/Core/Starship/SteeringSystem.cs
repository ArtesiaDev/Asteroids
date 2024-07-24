using Develop.Runtime.Core.Configs;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Core.Starship
{
    public class SteeringSystem : IFixedTickable
    {
        private readonly PlayerConfig _config;
        private readonly Transform _transform;

        public SteeringSystem(PlayerConfig config, Transform transform)
        {
            _config = config;
            _transform = transform;
        }
        
        public void FixedTick() =>
            Steer();

        private void Steer()
        {
            var input = Input.GetAxis("Horizontal");
            
            float rotation = -input * _config.RotationSpeed * Time.deltaTime;
            _transform.Rotate(0, 0, rotation);
        }
    }
}