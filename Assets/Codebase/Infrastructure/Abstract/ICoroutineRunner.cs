using System.Collections;
using UnityEngine;

namespace Codebase.Infrastructure.Abstract
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator enumerator);
        public void StopCoroutine(Coroutine enumerator);
    }
}