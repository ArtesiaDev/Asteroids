using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Develop.Runtime.Services.AssetManagement
{
    public interface ISceneProvider
    {
        public Task<SceneInstance> LoadScene(Scene scene);
    }
}