using System;
using Develop.Runtime.Core.Configs;
using Develop.Runtime.Services.Input.InputActions;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Core.Starship
{
    public class MoverSystem : IFixedTickable
    {
        public event Action PlayerMoved;
        
        private readonly IMoveConfig _config;
        private readonly IMoveAction _input;
        private readonly Rigidbody2D _rb;
        private readonly Transform _transform;

        public MoverSystem(IMoveConfig config, IMoveAction input, Rigidbody2D rb, Transform transform)
        {
            _config = config;
            _input = input;
            _rb = rb;
            _transform = transform;
        }

        public void FixedTick() =>
            Move();

        private void Move()
        {
            if (_input.Move)
            {
                Vector2 thrust = _transform.up * _config.ThrustPower;
                _rb.AddForce(thrust, ForceMode2D.Force);
            }
            
            PlayerMoved?.Invoke();
        }
    }
}