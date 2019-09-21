using System.Collections.Generic;
using UnityEngine;

namespace GX
{
    public static class CoroutineManager
    {
        private static Dictionary<object, Coroutine> dictionary = new Dictionary<object, Coroutine>();

        public static void StoreCoroutine(object key, Coroutine coroutine)
        {
            dictionary.Add(key, coroutine);
        }

        public static void StopCoroutine(object key)
        {
            GameManager.StopCoroutineGM(dictionary[key]);
            dictionary.Remove(key);
        }
    }
}
