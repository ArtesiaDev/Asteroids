namespace Develop.Backend.Analytics
{
    public interface IGamePlayAnalytics
    {
        public void LogGameStarted();
        public void LogGameSFinished(int laserUsedCount, int bulletUsedCount, int asteroidsDestroyedCount);
        public void LaserUsed();
    }
}