using System;
using Develop.Runtime.Core.Configs;
using Develop.Runtime.Services.Input.InputActions;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Core.Starship
{
    public class SteeringSystem : IFixedTickable
    {
        public static event Action PlayerSteered;
        
        private readonly ISteeringConfig _config;
        private readonly ISteerAction _input;
        private readonly Transform _transform;

        public SteeringSystem(ISteeringConfig config, ISteerAction input, Transform transform)
        {
            _config = config;
            _input = input;
            _transform = transform;
        }

        public void FixedTick() =>
            Steer();

        private void Steer()
        {
            float rotation = -_input.Steer * _config.RotationSpeed * Time.deltaTime;
            _transform.Rotate(0, 0, rotation);
            
            PlayerSteered?.Invoke();
        }
    }
}