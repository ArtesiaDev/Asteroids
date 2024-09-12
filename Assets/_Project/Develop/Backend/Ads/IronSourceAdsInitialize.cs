namespace Develop.Backend.Ads
{
    public class IronSourceAdsInitialize
    {
        private const string APP_KEY = "1f7d5ea95";
        public void Initialize()
        {
            IronSource.Agent.validateIntegration();
            IronSource.Agent.init(APP_KEY);
        }
    }
}