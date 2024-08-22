using System.Text;
using TMPro;
using UnityEngine;

namespace Develop.Runtime.Meta.Core
{
    public class ScoreView : MonoBehaviour
    {
        private TextMeshProUGUI _score;
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        private void Awake() =>
            _score = GetComponentInChildren<TextMeshProUGUI>();

        public void UpdateScore(int newValue)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("Score: {0}", newValue);
            _score.text = _stringBuilder.ToString();
        }
    }
}