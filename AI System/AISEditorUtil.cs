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
        public static T GetSingle<T>(AISController ctrl, int idx) where T : Object
        {
            if (idx < 0 || idx >= ctrl.variables.Count) return null;

            AISVarSingle var = ctrl.GetSingle(idx);
            if (!var) return null;

            return var.@object as T;
        }

        public static List<T> GetList<T>(AISController ctrl, int idx) where T : Object
        {
            if (idx < 0 || idx >= ctrl.variables.Count) return null;

            AISVarList vars = ctrl.GetList(idx);
            if (!vars) return null;

            List<T> list = vars.@object.Where(x => x is T).Select(x => (T)x).ToList();
            if (list == null || list.Count == 0) return null;

            return list;
        }


#if UNITY_EDITOR

        public static List<System.Type> allActions = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsSubclassOf(typeof(AISAction)) || x == typeof(AISAction)).ToList();

        public static List<System.Type> allScorers = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsSubclassOf(typeof(AISScorer)) || x == typeof(AISScorer)).ToList();

        public static List<System.Type> allVarType = new List<System.Type> { typeof(int), typeof(float), typeof(Vector3), typeof(GameObject), typeof(MotorSystem.MotorState), typeof(Hitbox) };
            /*System.AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).
            Where(x => (((x.IsSubclassOf(typeof(Component)) || x.IsValueType) && x.Namespace == "UnityEngine") || x.Namespace != null && x.Namespace.Contains("Game")) &&
            x.Namespace != null && !x.Namespace.Contains("Editor") && !x.Name.Contains("Editor") || x == typeof(int) || x == typeof(Transform)).ToList();*/


        public static void VarPopUp(string label, AISAI ai, ref int varIdx, bool list)
        {
            List<AISVariable> options;
            if (list) options = ai.variables.Where(x => x as AISVarList).ToList();
            else options = ai.variables.Where(x => x as AISVarSingle).ToList();

            Select(label, ai, ref varIdx, list, options);
        }
        
        public static void VarPopUp(string label, AISAI ai, ref int varIdx, bool list, System.Type type)
        {
            List<AISVariable> options;
            if (list) options = ai.variables.Where(x => x as AISVarList && x.type.Equals(type)).ToList();
            else options = ai.variables.Where(x => x as AISVarSingle && x.type.Equals(type)).ToList();

            Select(label, ai, ref varIdx, list, options);
        }

        private static void Select(string label, AISAI ai, ref int varIdx, bool list, List<AISVariable> options)
        {
            if (options != null && options.Count != 0)
            {
                if (varIdx < 0 || varIdx >= ai.variables.Count) varIdx = 0;
                AISVariable a = ai.variables[varIdx];

                if (!options.Contains(a)) a = options[0];

                a = options[EditorGUILayout.Popup(label + (list ? "(List): " : "(Single) "), options.IndexOf(a), options.Select(x => x.name).ToArray())];
                varIdx = ai.variables.IndexOf(a);
            }
            else { GUILayout.Label("There no Variable " + (list ? "Lists" : "Singles") + " and/or compatible types!"); varIdx = -1; }
        }
#endif
    }
}