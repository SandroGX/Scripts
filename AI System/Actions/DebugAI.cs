using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GX.AISystem
{
    [System.Serializable]
    public class DebugAI : AISAction
    {
        [SerializeField]
        string message;

        public override void OnAction(AISController controller)
        {
            base.OnAction(controller);
            Debug.Log(message);
        }

#if UNITY_EDITOR

        public override void GuiParameters()
        {
            message = EditorGUILayout.TextField("Message: ", message);
        }
#endif
    }
}
