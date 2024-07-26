using Develop.Runtime.Core.Spawn;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Installers.CoreContext
{
    public sealed class MonobehInstaller : MonoInstaller
    {
        public override void InstallBindings() =>
            BindMonobehavior<AsteroidSpawner>();

        private void BindMonobehavior<T>() where T : MonoBehaviour
        {
            var instance = FindObjectOfType<T>();

            if (instance == null)
                Debug.LogWarning($"Instance of type: {typeof(T)}  not found.");

            Container.BindInterfacesAndSelfTo<T>().FromInstance(instance).AsSingle();
        }
    }
}