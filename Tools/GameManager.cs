using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameObject PLAYER;
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (!instance)
                {
                    GameObject go = new GameObject("Game Manager");
                    DontDestroyOnLoad(go);
                    instance = go.AddComponent<GameManager>();
                }
                return instance;
            }
        }

        private void Awake()
        {
            if (!instance)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }

        public static Coroutine StartCoroutineGM(IEnumerator coroutine)
        {
            return Instance.StartCoroutine(coroutine);
        }

        public static void StopCoroutineGM(Coroutine coroutine)
        {
            Instance.StopCoroutine(coroutine);
        }
    }
}
