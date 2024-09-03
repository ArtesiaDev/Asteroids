using System;

namespace Develop.Runtime.EventSignals
{
    public class BulletSignals: IBulletSignals, IBulletSignalsHandler
    {
        public event Action BulletShot;

        public void OnBulletShot() =>
            BulletShot?.Invoke();
    }
}