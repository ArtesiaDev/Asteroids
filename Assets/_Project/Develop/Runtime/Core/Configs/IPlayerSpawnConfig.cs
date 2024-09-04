using UnityEngine;

namespace Develop.Runtime.Core.Configs
{
    public interface IPlayerSpawnConfig
    {
        public Vector2 SpawnPoint { get; }
        public float SpawnClearRadius { get; }
    }
}