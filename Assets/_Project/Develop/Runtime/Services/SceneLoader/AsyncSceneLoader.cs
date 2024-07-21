using System;
using System.Threading.Tasks;
using Develop.Runtime.Services.AssetManagement;
using UnityEngine.ResourceManagement.ResourceProviders;
using ISceneProvider = Develop.Runtime.Services.AssetManagement.ISceneProvider;

namespace Develop.Runtime.Services.SceneLoader
{
    public class AsyncSceneLoader : ISceneLoader
    {
        private readonly ISceneProvider _sceneProvider;
        
        public AsyncSceneLoader(ISceneProvider sceneProvider)
        {
            _sceneProvider = sceneProvider;
        }

        public async Task<SceneInstance> Load(Scene name, Action onLoaded = null)
        {
           var scene = await _sceneProvider.LoadScene(name);
           scene.ActivateAsync();
           onLoaded?.Invoke();
           return scene;
        }
    }
}