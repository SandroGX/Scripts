using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Game.SistemaMotor;

public class AnimEstadoEditor : Editor
{
    bool mostrar;

    [CustomEditor(typeof(AnimEstado))]
    public override void OnInspectorGUI()
    {
        AnimEstado myTarget = (AnimEstado)target;

        mostrar = EditorGUILayout.Foldout(mostrar, "Condicoes");
        if (mostrar)
        {
            foreach (Anim a in myTarget.condicoes)
            {
                a.mostrar = EditorGUILayout.Foldout(a.mostrar, a.paramNome);
                if (a.mostrar)
                {
                    switch (a.Var)
                    {
                        case (Anim.p.Bool): a.Boolean = EditorGUILayout.Toggle(a.Boolean); break;
                        case (Anim.p.Int): a.Integer = EditorGUILayout.IntField(a.Integer); break;
                        case (Anim.p.Float): a.Float = EditorGUILayout.FloatField(a.Float); break;

                    }
                }
            }
        }
    }
}
