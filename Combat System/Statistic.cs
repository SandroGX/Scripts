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
        value = original.value;
        minValue = original.minValue;
        maxValue = original.maxValue;
        varValue = original.varValue;
        Min = null;
        Max = null;
    }

    public Statistic()
    {
        value = 0;
        minValue = 0;
        maxValue = 0;
        varValue = 0;
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
        if (value == minValue && Min != null) { Min(); }
        if (value == maxValue && Max != null) {Max(); }
    }


#if UNITY_EDITOR
    public void Gui()
    {
        UnityEditor.EditorGUILayout.BeginHorizontal();

        value = UnityEditor.EditorGUILayout.Slider("Value: ", value, minValue, maxValue);
        minValue = UnityEditor.EditorGUILayout.FloatField("Min Value: ", minValue);
        maxValue = UnityEditor.EditorGUILayout.FloatField("Max Value: ", maxValue);
        varValue = UnityEditor.EditorGUILayout.FloatField("Var Value: ", varValue);

        UnityEditor.EditorGUILayout.EndHorizontal();
    }

#endif 
}
