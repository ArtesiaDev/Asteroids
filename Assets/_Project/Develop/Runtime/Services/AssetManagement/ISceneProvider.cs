using System.Threading.Tasks;
using Develop.Runtime.Services.SceneLoader;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Develop.Runtime.Services.AssetManagement
{
    public interface ISceneProvider
    {
        public Task<SceneInstance> LoadScene(Scene scene);
    }
}