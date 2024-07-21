using System;
using System.Threading.Tasks;
using Develop.Runtime.Services.AssetManagement;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Develop.Runtime.Services.SceneLoader
{
    public interface ISceneLoader
    {
        public Task<SceneInstance> Load(Scene scene, Action onLoaded = null);
    }
}