using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Linq;
using System.Collections.Generic;

namespace Game.AISystem
{
    public static class AISEditorUtil
    {
        public static int GetSingleInt<T>(AISController ctrl, AISVariable key)
        {
            if (!ctrl.ContainsKey(key)) return 0;

            AISVarSingle var = (AISVarSingle)ctrl.GetVar(key);
            return var.@int;
        }

        public static List<int> GetListInt(AISController ctrl, AISVariable key)
        {
            if (!ctrl.ContainsKey(key)) return null;

            AISVarList vars = (AISVarList)ctrl.GetVar(key);
            return vars.@int;
        }

        public static float GetSingleFloat(AISController ctrl, AISVariable key)
        {
            if (!ctrl.ContainsKey(key)) return 0;

            AISVarSingle var = (AISVarSingle)ctrl.GetVar(key);
            return var.@float;
        }

        public static List<float> GetListFloat(AISController ctrl, AISVariable key)
        {
            if (!ctrl.ContainsKey(key)) return null;

            AISVarList vars = (AISVarList)ctrl.GetVar(key);
            return vars.@float;
        }

        public static Vector3 GetSingleVector3(AISController ctrl, AISVariable key)
        {
            if (!ctrl.ContainsKey(key)) return Vector3.zero;

            AISVarSingle var = (AISVarSingle)ctrl.GetVar(key);
            return var.vector3;
        }

        public static List<Vector3> GetListVector3(AISController ctrl, AISVariable key)
        {
            if (!ctrl.ContainsKey(key)) return null;

            AISVarList vars = (AISVarList)ctrl.GetVar(key);
            return vars.vector3;
        }

        public static T GetSingleObject<T>(AISController ctrl, AISVariable key) where T : Object
        {
            if (!ctrl.ContainsKey(key)) return null;

            AISVarSingle var = (AISVarSingle)ctrl.GetVar(key);
            return var.@object as T;
        }

        public static List<T> GetListObject<T>(AISController ctrl, AISVariable key) where T : Object
        {
            if (!ctrl.ContainsKey(key)) return null;

            AISVarList vars = (AISVarList)ctrl.GetVar(key);

            List<T> list = vars.@object.Where(x => x is T).Select(x => (T)x).ToList();
            if (list == null || list.Count == 0) return null;

            return list;
        }

        public static T GetComponent<T>(AISController ctrl, AISVariable key) where T : Component
        {
            return ((AISVarComponent)ctrl.GetVar(key)).GetComponent<T>();
        }

        public static T GetItemComponent<T>(AISController ctrl, AISVariable key) where T : InventorySystem.ItemComponent
        {
            return ((AISVarComponent)ctrl.GetVar(key)).GetItemComponent<T>();
        }


#if UNITY_EDITOR

        public static List<System.Type> allActions = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsSubclassOf(typeof(AISAction)) || x == typeof(AISAction)).ToList();

        public static List<System.Type> allScorers = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsSubclassOf(typeof(AISScorer)) || x == typeof(AISScorer)).ToList();

        public static List<string> allVarType = new List<string> { typeof(int).FullName, typeof(float).FullName, "UnityEngine.Transform, UnityEngine", typeof(MotorSystem.MotorState).FullName, typeof(Hitbox).FullName,
        typeof(Character).FullName };
            

        public static AISVariable VarPopUp(string label, AISAI ai, AISVariable var, System.Type t)
        {
            return Select(label, ai, var, t, ai.variables.Where(x => x.GetType() == t).ToList());
        }
        
        public static AISVariable VarPopUp(string label, AISAI ai, AISVariable var, System.Type t, System.Type type)
        {
            return Select(label, ai, var, t, ai.variables.Where(x => x.GetType() == t && System.Type.GetType(x.type).Equals(type)).ToList());
        }

        private static AISVariable Select(string label, AISAI ai, AISVariable var, System.Type t, List<AISVariable> options)
        {
            if (options != null && options.Count != 0)
            {
                if (!ai.variables.Contains(var)) var = ai.variables[0];

                return options[EditorGUILayout.Popup(label + "(" + t + ")", options.IndexOf(var), options.Select(x => x.name).ToArray())];
            }
            else { GUILayout.Label("There no Variable " + "(" + t + ")" + " and/or compatible types!"); return null; }
        }
#endif
    }
}