using System.Collections;
using UnityEngine;

namespace Services.Essential
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}