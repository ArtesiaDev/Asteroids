namespace Develop.Backend.Ads
{
    public interface IAdsService
    {
        public void LoadBanner();
        public void DestroyBanner();
        public void LoadInterstitial();
        public void ShowInterstitial();
        public void LoadRewarded();
        public void ShowRewarded();
    }
}