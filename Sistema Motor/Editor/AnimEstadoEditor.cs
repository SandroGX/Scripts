using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Game.MotorSystem;

public class AnimEstadoEditor : Editor
{
    bool mostrar;

    [CustomEditor(typeof(AnimState))]
    public override void OnInspectorGUI()
    {
        //AnimEstado myTarget = (AnimEstado)target;

        //mostrar = EditorGUILayout.Foldout(mostrar, "Condicoes");
        //if (mostrar)
        //{
        //    foreach (Anim a in myTarget.condicoes)
        //    {
        //        a.mostrar = EditorGUILayout.Foldout(a.mostrar, a.paramNome);
        //        if (a.mostrar)
        //        {
        //            switch (a.Var)
        //            {
        //                case (Anim.VarType.Bool): a.Boolean = EditorGUILayout.Toggle(a.Boolean); break;
        //                case (Anim.VarType.Int): a.Integer = EditorGUILayout.IntField(a.Integer); break;
        //                case (Anim.VarType.Float): a.Float = EditorGUILayout.FloatField(a.Float); break;

        //            }
        //        }
        //    }
        //}
    }
}
