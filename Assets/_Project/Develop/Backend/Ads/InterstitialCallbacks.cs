using System;
using Develop.Backend.Analytics;
using Zenject;

namespace Develop.Backend.Ads
{
    public class InterstitialCallbacks: IInitializable, IDisposable
    {
        private IAdsAnalytics _analytics;

        [Inject]
        private void Construct(IAdsAnalytics analytics) =>
            _analytics = analytics;
        
        public void Initialize()
        {
            IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
            IronSourceInterstitialEvents.onAdLoadFailedEvent += InterstitialOnAdLoadFailedEvent;
            IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialOnAdOpenedEvent;
            IronSourceInterstitialEvents.onAdClickedEvent += InterstitialOnAdClickedEvent;
            IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
            IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialOnAdShowFailedEvent;
            IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;
        }

        public void Dispose()
        {
            IronSourceInterstitialEvents.onAdReadyEvent -= InterstitialOnAdReadyEvent;
            IronSourceInterstitialEvents.onAdLoadFailedEvent -= InterstitialOnAdLoadFailedEvent;
            IronSourceInterstitialEvents.onAdOpenedEvent -= InterstitialOnAdOpenedEvent;
            IronSourceInterstitialEvents.onAdClickedEvent -= InterstitialOnAdClickedEvent;
            IronSourceInterstitialEvents.onAdShowSucceededEvent -= InterstitialOnAdShowSucceededEvent;
            IronSourceInterstitialEvents.onAdShowFailedEvent -= InterstitialOnAdShowFailedEvent;
            IronSourceInterstitialEvents.onAdClosedEvent -= InterstitialOnAdClosedEvent;
        }
        
        // Invoked when the interstitial ad was loaded successfully.
        private void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo)
        {
        }

        // Invoked when the initialization process has failed.
        private void InterstitialOnAdLoadFailedEvent(IronSourceError ironSourceError)
        {
        }

        // Invoked when the Interstitial Ad Unit has opened. This is the impression indication. 
        private void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo)
        {
            _analytics.LogInterstitialAdStarted(adInfo);
        }

        // Invoked when end user clicked on the interstitial ad
        private void InterstitialOnAdClickedEvent(IronSourceAdInfo adInfo)
        {
            _analytics.LogInterstitialAdClicked(adInfo);
        }

        // Invoked when the ad failed to show.
        private void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
        {
        }

        // Invoked when the interstitial ad closed and the user went back to the application screen.
        private void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo)
        {
        }

        // Invoked before the interstitial ad was opened, and before the InterstitialOnAdOpenedEvent is reported.
        // This callback is not supported by all networks, and we recommend using it only if  
        // it's supported by all networks you included in your build. 
        private void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo)
        {
        }
    }
}