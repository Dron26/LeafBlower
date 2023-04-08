using System.Collections;
using UnityEngine;

namespace Infrastructure
{
    public interface ICorountineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}