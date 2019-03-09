using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace Game
{
    //class to easily start a coroutine with a certain method and add IClocks to it
    public class Clock<T> where T : IClock
    {
        //time to wait
        private WaitForSeconds wait;

        //coroutine
        private Coroutine coroutine;

        //method to call per tick
        private Action<T> action;

        List<T> list = new List<T>();

        //constructors
        public Clock(Action<T> action, float tickTime) { this.action = action; wait = new WaitForSeconds(tickTime); }

        //start coroutine
        private void StartTick()
        {
            coroutine = GameManager.StartCoroutineGM(Tick());
        }

        //stop coroutine
        private void StopTick()
        {
            GameManager.StopCoroutineGM(coroutine);
            coroutine = null;
        }

        //add t for coroutine to call
        public void Add(T t)
        {
            list.Add(t);
            if (list.Count == 1)
                StartTick();
        }

        //remove t from coroutine to call
        public void Remove(T t)
        {
            list.Remove(t);
            if (list.Count == 0)
                StopTick();
        }

        //coroutine
        private System.Collections.IEnumerator Tick()
        {
            while (true)
            {
                foreach(T t in list)
                    action(t);
                yield return wait;
            }
        }
    }

    //UnityObject that wants to get called
    public interface IClock
    {
        //make sure that it has GetInstanceID() method
        int GetInstanceID();
    }
}