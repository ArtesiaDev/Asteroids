using Firebase.Analytics;

namespace Develop.Backend
{
    public class FirebaseAnalyticsService : IAnalyticsService
    {
        public void LogGameStart()
        {
            FirebaseAnalytics.LogEvent("sign_up");
        }

        public void LogGameEnd(int shotsFired, int laserUsed)
        {
            var parameters = new Parameter[]
            {
                new Parameter("shots_fired", shotsFired),
                new Parameter("laser_used", laserUsed)
            };
            FirebaseAnalytics.LogEvent("game_end", parameters);
        }

        public void LogLaserUsed()
        {
            FirebaseAnalytics.LogEvent("laser_used");
        }
    }

}