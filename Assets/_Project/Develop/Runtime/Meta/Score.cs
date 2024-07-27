using System.Text;
using Develop.Runtime.Core.Spawn;
using TMPro;
using UnityEngine;

namespace Develop.Runtime.Meta
{
    public class Score : MonoBehaviour, IScoreCountable
    {
        private TextMeshProUGUI _score;
        private readonly StringBuilder _stringBuilder = new StringBuilder();
        public int ScoreCount { get; private set; }

        private void Awake()
        {
            _score = GetComponentInChildren<TextMeshProUGUI>();
            ScoreCount = 0;
        }

        private void OnEnable()
        {
            Asteroid.AsteroidDied += UpdateScore;
            Asteroid.PlayerDied += SwitchOf;
        }

        private void OnDisable()
        {
            Asteroid.AsteroidDied -= UpdateScore;
            Asteroid.PlayerDied -= SwitchOf;
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