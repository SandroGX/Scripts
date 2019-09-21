using UnityEngine;
using System;
using System.Collections;
using GX;

public static class CoroutineAux
{

    public static Coroutine StartCoroutineLoop(Action a, object yieldInstruction)
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction));
    }
    private static IEnumerator CoroutineLoop(Action a, object yieldInstruction)
    {
        while (true)
        {
            yield return yieldInstruction;
            a();
        }
    }


    public static Coroutine StartCoroutineLoop<T0>(
        Action<T0> a,
        object yieldInstruction,
        T0 t0
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0));
    }
    private static IEnumerator CoroutineLoop<T0>(
        Action<T0> a,
        object yieldInstruction,
        T0 t0
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0);
        }
    }


    public static Coroutine StartCoroutineLoop<T0, T1>(
        Action<T0, T1> a,
        object yieldInstruction,
        T0 t0, T1 t1
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0, t1));
    }
    private static IEnumerator CoroutineLoop<T0, T1>(
        Action<T0, T1> a,
        object yieldInstruction,
        T0 t0, T1 t1
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0, t1);
        }
    }


    public static Coroutine StartCoroutineLoop<T0, T1, T2>(
        Action<T0, T1, T2> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0, t1, t2));
    }
    private static IEnumerator CoroutineLoop<T0, T1, T2>(
        Action<T0, T1, T2> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0, t1, t2);
        }
    }


    public static Coroutine StartCoroutineLoop<T0, T1, T2, T3>(
        Action<T0, T1, T2, T3> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0, t1, t2, t3));
    }
    private static IEnumerator CoroutineLoop<T0, T1, T2, T3>(
        Action<T0, T1, T2, T3> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0, t1, t2, t3);
        }
    }

    public static Coroutine StartCoroutineLoop<T0, T1, T2, T3, T4>(
        Action<T0, T1, T2, T3, T4> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0, t1, t2, t3, t4));
    }
    private static IEnumerator CoroutineLoop<T0, T1, T2, T3, T4>(
        Action<T0, T1, T2, T3, T4> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0, t1, t2, t3, t4);
        }
    }


    public static Coroutine StartCoroutineLoop<T0, T1, T2, T3, T4, T5>(
        Action<T0, T1, T2, T3, T4, T5> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0, t1, t2, t3, t4, t5));
    }
    private static IEnumerator CoroutineLoop<T0, T1, T2, T3, T4, T5>(
        Action<T0, T1, T2, T3, T4, T5> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0, t1, t2, t3, t4, t5);
        }
    }


    public static Coroutine StartCoroutineLoop<T0, T1, T2, T3, T4, T5, T6>(
        Action<T0, T1, T2, T3, T4, T5, T6> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0, t1, t2, t3, t4, t5, t6));
    }
    private static IEnumerator CoroutineLoop<T0, T1, T2, T3, T4, T5, T6>(
        Action<T0, T1, T2, T3, T4, T5, T6> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0, t1, t2, t3, t4, t5, t6);
        }
    }


    public static Coroutine StartCoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0, t1, t2, t3, t4, t5, t6, t7));
    }
    private static IEnumerator CoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0, t1, t2, t3, t4, t5, t6, t7);
        }
    }

    public static Coroutine StartCoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0, t1, t2, t3, t4, t5, t6, t7, t8));
    }
    private static IEnumerator CoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0, t1, t2, t3, t4, t5, t6, t7, t8);
        }
    }


    public static Coroutine StartCoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0, t1, t2, t3, t4, t5, t6, t7, t8, t9));
    }
    private static IEnumerator CoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0, t1, t2, t3, t4, t5, t6, t7, t8, t9);
        }
    }


    public static Coroutine StartCoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10));
    }
    private static IEnumerator CoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10);
        }
    }


    public static Coroutine StartCoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11));
    }
    private static IEnumerator CoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
        }
    }


    public static Coroutine StartCoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12));
    }
    private static IEnumerator CoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12);
        }
    }


    public static Coroutine StartCoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13));
    }
    private static IEnumerator CoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13);
        }
    }


    public static Coroutine StartCoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14));
    }
    private static IEnumerator CoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14);
        }
    }


    public static Coroutine StartCoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> a, 
        object yieldInstruction, 
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15));
    }
    private static IEnumerator CoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15);
        }
    }

    /*
    public static Coroutine StartCoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16
        )
    {
        return GameManager.StartCoroutineGM(CoroutineLoop(a, yieldInstruction, t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16));
    }
    private static IEnumerator CoroutineLoop<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
        Action<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> a,
        object yieldInstruction,
        T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16
        )
    {
        while (true)
        {
            yield return yieldInstruction;
            a(t0, t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16);
        }
    }
    */
}
