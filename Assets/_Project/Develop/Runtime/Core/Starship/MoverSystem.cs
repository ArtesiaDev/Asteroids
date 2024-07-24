﻿using Develop.Runtime.Core.Configs;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Core.Starship
{
    public class MoverSystem : IFixedTickable
    {
        private readonly PlayerConfig _config;
        private readonly Rigidbody2D _rb;
        private readonly Transform _transform;

        public MoverSystem(PlayerConfig config, Rigidbody2D rb, Transform transform)
        {
            _config = config;
            _rb = rb;
            _transform = transform;
        }

        public void FixedTick() =>
            Move();

        private void Move()
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                Vector2 thrust = _transform.up * _config.ThrustPower;
                _rb.AddForce(thrust, ForceMode2D.Force);
            }
        }
    }
}