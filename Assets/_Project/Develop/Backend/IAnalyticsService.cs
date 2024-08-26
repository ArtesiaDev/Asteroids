namespace Develop.Backend
{
    public interface IAnalyticsService
    {
       public void LogGameStart();
       public void LogGameEnd(int shotsFired, int laserUsed);
       public void LogLaserUsed();
    }
}