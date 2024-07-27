using System.Collections;
using UnityEngine;

namespace Develop.Runtime.Services.CoroutinePerformer
{
    public interface ICoroutinePerformer
    {
        public Coroutine StartPerform(IEnumerator coroutine);

        public void StopPerform(Coroutine coroutine);
    }
}