using UnityEngine;

namespace Develop.Runtime.Meta.Core
{
    public class CoreModel
    {
        public int Score { get; private set; }
        
        public int BulletUsedCount { get; private set; }
        
        public int LaserUsedCount { get; private set; }

        public void ScoreChange(int newValue)
        {
            if (IsValidateInt(newValue, Score))
                Score = newValue;
        }
        public void BulletUsedCountChange(int newValue)
        {
            if (IsValidateInt(newValue, BulletUsedCount))
                BulletUsedCount = newValue;
        }
        public void LaserUsedCountChange(int newValue)
        {
            if (IsValidateInt(newValue, LaserUsedCount))
                LaserUsedCount = newValue;
        }

        private bool IsValidateInt(int newValue, int data) =>
            Mathf.Abs(newValue - data) == 0 || Mathf.Abs(newValue - data) == 1;
    }
}