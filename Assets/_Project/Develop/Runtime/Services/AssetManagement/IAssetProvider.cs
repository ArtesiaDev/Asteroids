using System.Threading.Tasks;

namespace Develop.Runtime.Services.AssetManagement
{
    public interface IAssetProvider : ISceneProvider
    {
        public Task<T> Load<T>(string key) where T : class;
        public void Release(string key);
    }
}