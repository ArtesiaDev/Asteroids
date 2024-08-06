using System;
using Develop.Runtime.Core.Configs;
using Develop.Runtime.EventSignals;
using Develop.Runtime.Services.Input.InputActions;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Core.Starship
{
    public class SteeringSystem : IFixedTickable, IDisposable
    {
        public event Action PlayerSteered;

        private readonly ISteeringConfig _config;
        private readonly ISteerAction _input;
        private readonly Transform _transform;
        private readonly IPlayerSignalsHandler _playerSignalsHandler;


        public SteeringSystem(ISteeringConfig config, ISteerAction input, Transform transform,
            IPlayerSignalsHandler playerSignalsHandler)
        {
            _config = config;
            _input = input;
            _transform = transform;
            _playerSignalsHandler = playerSignalsHandler;
            PlayerSteered += _playerSignalsHandler.OnPlayerSteered;
        }

        public void Dispose() =>
            PlayerSteered -= _playerSignalsHandler.OnPlayerSteered;

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