using System.Collections.Generic;
using UnityEngine;

namespace Develop.Runtime.Core.Spawn
{
    public abstract class Spawner : MonoBehaviour
    {
        protected Dictionary<AsteroidTypes, string> InitializeAsteroids()
        {
            return new Dictionary<AsteroidTypes, string>
            {
                { AsteroidTypes.AsterHuge1, "AsterHuge1" },
                { AsteroidTypes.AsterHuge2, "AsterHuge2" },
                { AsteroidTypes.AsterHuge3, "AsterHuge3" },
                { AsteroidTypes.AsterMed1, "AsterMed1" },
                { AsteroidTypes.AsterMed2, "AsterMed2" },
                { AsteroidTypes.AsterMed3, "AsterMed3" },
                { AsteroidTypes.AsterSmall1, "AsterSmall1" },
                { AsteroidTypes.AsterSmall2, "AsterSmall2" },
                { AsteroidTypes.AsterSmall3, "AsterSmall3" },
            };
        }
    }
}