namespace Develop.Backend.Ads
{
    public class IronSourceAdsInitialize
    {
#if UNITY_ANDROID
        private const string APP_KEY = "1f7d5ea95";
#endif

        public void Initialize()
        {
            IronSource.Agent.validateIntegration();
            IronSource.Agent.init(APP_KEY);
        }
    }
}