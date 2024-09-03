using UnityEngine;

namespace Develop.Runtime.Meta.Core
{
    public class CoreUIModel
    {
        public int Score { get; private set; }

        public void ScoreChange(int newValue)
        {
            if (IsValidateScore(newValue))
                Score = newValue;
        }

        private bool IsValidateScore(int newValue) =>
            Mathf.Abs(newValue - Score) == 0 || Mathf.Abs(newValue - Score) == 1;
    }
}