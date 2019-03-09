using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameObject PLAYER;
        public static float STAT_VAR_TIME = 0.2f;
        public static GameManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }

        public static Coroutine StartCoroutineGM(IEnumerator coroutine)
        {
            return instance.StartCoroutine(coroutine);
        }

        public static void StopCoroutineGM(Coroutine coroutine)
        {
            instance.StopCoroutine(coroutine);
        }
    }
}
