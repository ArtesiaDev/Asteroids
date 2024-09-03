namespace Develop.Backend.Analytics
{
    public interface IAdsAnalytics
    {
        public void LogRewardedVideoStarted(IronSourceAdInfo adInfo);
        public void LogRewardedVideoClicked(IronSourceAdInfo adInfo);
        public void LogInterstitialAdStarted(IronSourceAdInfo adInfo);
        public void LogInterstitialAdClicked(IronSourceAdInfo adInfo);
        public void LogBannerLoaded(IronSourceAdInfo adInfo);
        public void LogBannerClicked(IronSourceAdInfo adInfo);
    }
}