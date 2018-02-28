using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Game.MotorSystem;

namespace Game.AISystem
{
    [System.Serializable]
    public class ChangeState : AISAction
    {
        [SerializeField]
        MotorState state;

        public override void OnActionEnter(AISController controller)
        {
            base.OnActionEnter(controller);
            controller.motor.nextState = state;
        }


        public override void OnAction(AISController controller)
        {
            base.OnAction(controller);
            if (controller.motor.currentState != state) controller.motor.nextState = state;
        }

#if UNITY_EDITOR
        public override void GuiParameters()
        {
            base.GuiParameters();

            state = EditorGUILayout.ObjectField(state, typeof(MotorState), false) as MotorState;
        }
#endif

    }
}
