using System.Collections.Generic;
using GameAnalyticsSDK;

namespace Develop.Backend.Analytics
{
    public class GameAnalyticsGamePlay: IGamePlayAnalytics
    {
        public void LogGameStarted() =>
            GameAnalytics.NewDesignEvent("GameStarted");

        public void LogGameSFinished(int laserUsedCount, int bulletUsedCount, int asteroidsDestroyedCount)
        {
            GameAnalytics.NewDesignEvent("GameFinished", new Dictionary<string, object>()
            {
                ["LaserUsedCount"] = laserUsedCount,
                ["BulletUsedCount"] = bulletUsedCount,
                ["AsteroidsDestroyedCount"] = asteroidsDestroyedCount
            });
        }

        public void LaserUsed() =>
            GameAnalytics.NewDesignEvent("LaserUsed");
    }
}