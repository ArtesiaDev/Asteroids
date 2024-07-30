using System.Text;
using Develop.Runtime.Meta.EventSignals;
using TMPro;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Meta
{
    public class Score : MonoBehaviour, IScoreCountable
    {
        private TextMeshProUGUI _score;
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        private IPlayerSignals _playerSignals;
        private IAsteroidSignals _asteroidSignals;
        public int ScoreCount { get; private set; }

        [Inject]
        private void Construct(IPlayerSignals playerSignals, IAsteroidSignals asteroidSignals)
        {
            _playerSignals = playerSignals;
            _asteroidSignals = asteroidSignals;
        }
        private void Awake()
        {
            _score = GetComponentInChildren<TextMeshProUGUI>();
            ScoreCount = 0;
        }

        private void OnEnable()
        {
            _asteroidSignals.AsteroidDied += UpdateScore;
            _playerSignals.PlayerDied += SwitchOf;
        }

        private void OnDisable()
        {
            _asteroidSignals.AsteroidDied -= UpdateScore;
            _playerSignals.PlayerDied -= SwitchOf;
        }

        private void UpdateScore()
        {
            ScoreCount++;
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("Score: {0}", ScoreCount);
            _score.text = _stringBuilder.ToString();
        }

        private void SwitchOf() =>
            gameObject.SetActive(false);
    }
}