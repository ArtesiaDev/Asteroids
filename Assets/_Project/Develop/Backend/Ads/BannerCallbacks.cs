using System;
using Develop.Backend.Analytics;
using Zenject;

namespace Develop.Backend.Ads
{
    public class BannerCallbacks: IInitializable, IDisposable
    {
        private IAdsAnalytics _analytics;

        [Inject]
        private void Construct(IAdsAnalytics analytics) =>
            _analytics = analytics;
        
        public void Initialize()
        {
            IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
            IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
            IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
            IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
            IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
            IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;
        }

        public void Dispose()
        {
            IronSourceBannerEvents.onAdLoadedEvent -= BannerOnAdLoadedEvent;
            IronSourceBannerEvents.onAdLoadFailedEvent -= BannerOnAdLoadFailedEvent;
            IronSourceBannerEvents.onAdClickedEvent -= BannerOnAdClickedEvent;
            IronSourceBannerEvents.onAdScreenPresentedEvent -= BannerOnAdScreenPresentedEvent;
            IronSourceBannerEvents.onAdScreenDismissedEvent -= BannerOnAdScreenDismissedEvent;
            IronSourceBannerEvents.onAdLeftApplicationEvent -= BannerOnAdLeftApplicationEvent;
        }
        
        //Invoked once the banner has loaded
        private void BannerOnAdLoadedEvent(IronSourceAdInfo adInfo)
        {
            _analytics.LogBannerLoaded(adInfo);
        }

        //Invoked when the banner loading process has failed.
        private void BannerOnAdLoadFailedEvent(IronSourceError ironSourceError)
        {
        }

        // Invoked when end user clicks on the banner ad
        private void BannerOnAdClickedEvent(IronSourceAdInfo adInfo)
        {
            _analytics.LogBannerClicked(adInfo);
        }

        //Notifies the presentation of a full screen content following user click
        private void BannerOnAdScreenPresentedEvent(IronSourceAdInfo adInfo)
        {
        }

        //Notifies the presented screen has been dismissed
        private void BannerOnAdScreenDismissedEvent(IronSourceAdInfo adInfo)
        {
        }

        //Invoked when the user leaves the app
        private void BannerOnAdLeftApplicationEvent(IronSourceAdInfo adInfo)
        {
        }
    }
}