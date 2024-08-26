using Firebase;
using Firebase.Analytics;
using UnityEngine;

namespace Develop.Backend
{
    public class FirebaseInitializer : MonoBehaviour
    {
        public void Awake()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            });
        }
    }
}