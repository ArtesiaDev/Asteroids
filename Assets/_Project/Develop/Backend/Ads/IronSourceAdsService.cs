using UnityEngine;

namespace Develop.Backend.Ads
{
    public class IronSourceAdsService: IAdsService
    {
        public void LoadBanner() =>
            IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);

        public void DestroyBanner() =>
            IronSource.Agent.destroyBanner();

        public void LoadInterstitial() =>
            IronSource.Agent.loadInterstitial();

        public void ShowInterstitial()
        {
            if (IronSource.Agent.isInterstitialReady())
                IronSource.Agent.showInterstitial();
            else Debug.LogWarning("Interstitial not ready");
        }

        public void LoadRewarded() =>
            IronSource.Agent.loadRewardedVideo();

        public void ShowRewarded()
        {
            if (IronSource.Agent.isRewardedVideoAvailable())
                IronSource.Agent.showRewardedVideo();
            else Debug.LogWarning("Rewarded video not ready");
        }
    }
}