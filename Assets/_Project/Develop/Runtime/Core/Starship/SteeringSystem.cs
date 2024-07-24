using Develop.Runtime.Core.Configs;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Core.Starship
{
    public class SteeringSystem : IFixedTickable
    {
        private readonly ISteeringConfig _config;
        private readonly Transform _transform;

        public SteeringSystem(ISteeringConfig config, Transform transform)
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