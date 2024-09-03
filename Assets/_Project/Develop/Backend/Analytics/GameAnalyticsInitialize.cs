using GameAnalyticsSDK;
using UnityEngine;

namespace Develop.Backend.Analytics
{
    public sealed class GameAnalyticsInitialize : MonoBehaviour
    {
        private void Awake() =>
            GameAnalytics.Initialize();
    }
}