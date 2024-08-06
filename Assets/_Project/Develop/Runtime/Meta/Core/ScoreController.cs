using System;
using Develop.Runtime.Meta.EventSignals;
using Zenject;

namespace Develop.Runtime.Meta.Core
{
    public class ScoreController : IInitializable, IDisposable
    {
        private IPlayerSignals _playerSignals;
        private IAsteroidSignals _asteroidSignals;
        private ScoreView _view;
        private CoreUIModel _model;

        [Inject]
        private void Construct(IPlayerSignals playerSignals, IAsteroidSignals asteroidSignals, ScoreView view,
            CoreUIModel model)
        {
            _playerSignals = playerSignals;
            _asteroidSignals = asteroidSignals;
            _view = view;
            _model = model;
        }

        public void Initialize()
        {
            _asteroidSignals.AsteroidDied += OnAsteroidDied;
            _playerSignals.PlayerDied += OnPlayerDied;
        }

        public void Dispose()
        {
            _asteroidSignals.AsteroidDied -= OnAsteroidDied;
            _playerSignals.PlayerDied -= OnPlayerDied;
        }

        private void OnAsteroidDied()
        {
           _model.ScoreChange(_model.Score + 1);
           _view.UpdateScore(_model.Score);
        }

        private void OnPlayerDied() =>
            _view.gameObject.SetActive(false);
    }
}