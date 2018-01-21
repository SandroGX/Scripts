using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Statistic
{
    public float value, minValue, maxValue, varValue;
    public System.Action Min, Max;

    public void Add(float B)
    {
        value += B; 
        Limits();
    }


    public Statistic(float value, float minValue, float maxValue, float varValue)
    {
        this.value = Mathf.Clamp(value, minValue, maxValue);
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.varValue = varValue;
        Min = null;
        Max = null;
    }

    public Statistic(Statistic original)
    {
        this.value = original.value;
        this.minValue = original.minValue;
        this.maxValue = original.maxValue;
        this.varValue = original.varValue;
        Min = null;
        Max = null;
    }

    public Statistic()
    {
        this.value = 0;
        this.minValue = 0;
        this.maxValue = 0;
        this.varValue = 0;
        Min = null;
        Max = null;
    }

    public IEnumerator Variation()
    {
        while (true)
        {
            value += varValue * GameManager.STAT_VAR_TIME;
            Limits();
            yield return new WaitForSeconds(GameManager.STAT_VAR_TIME);
        }

    }


    void Limits()
    {
        value = Mathf.Clamp(value, minValue, maxValue);
        if (value == minValue && Min != null) Min();
        if (value == maxValue && Max != null) Max();
    }


#if UNITY_EDITOR
    public void Gui()
    {
        UnityEditor.EditorGUILayout.BeginHorizontal();

        value = UnityEditor.EditorGUILayout.FloatField("Value: ", value);
        minValue = UnityEditor.EditorGUILayout.FloatField("Min Value: ", minValue);
        maxValue = UnityEditor.EditorGUILayout.FloatField("Max Value: ", maxValue);
        varValue = UnityEditor.EditorGUILayout.FloatField("Var Value: ", varValue);

        UnityEditor.EditorGUILayout.EndHorizontal();
    }

#endif 
}
