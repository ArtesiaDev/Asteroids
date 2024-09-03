using GameAnalyticsSDK;

namespace Develop.Backend.Analytics
{
    public class GameAnalyticsAds: IAdsAnalytics
    {
        public void LogRewardedVideoStarted(IronSourceAdInfo adInfo) =>
            GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo, adInfo.adNetwork, "Rewarded_Android");

        public void LogRewardedVideoClicked(IronSourceAdInfo adInfo) =>
            GameAnalytics.NewAdEvent(GAAdAction.Clicked, GAAdType.RewardedVideo, adInfo.adNetwork, "Rewarded_Android");

        public void LogInterstitialAdStarted(IronSourceAdInfo adInfo) =>
            GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, adInfo.adNetwork, "Interstitial_Android");

        public void LogInterstitialAdClicked(IronSourceAdInfo adInfo) =>
            GameAnalytics.NewAdEvent(GAAdAction.Clicked, GAAdType.Interstitial, adInfo.adNetwork, "Interstitial_Android");

        public void LogBannerLoaded(IronSourceAdInfo adInfo) =>
            GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Banner, adInfo.adNetwork, "Banner_Android");

        public void LogBannerClicked(IronSourceAdInfo adInfo) =>
            GameAnalytics.NewAdEvent(GAAdAction.Clicked, GAAdType.Banner, adInfo.adNetwork, "Banner_Android");
    }
}