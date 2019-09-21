using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Game.StateMachineSystem {
    //popup to add behaviour to state
    public class AddConditionPopup : PopupWindowContent
    {
        //List of all types inheriting from Condition
        private static readonly List<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsSubclassOf(typeof(TransitionCondition)) && !x.IsAbstract)
            .ToList();
        //transition to add new condition
        private readonly int t;
        //transition editor of t
        private readonly TransitionEditor te;
        //currently selected type
        private Type condition = types[0];

        //create popup for transition t of editor te
        public AddConditionPopup(TransitionEditor te, int t) { this.te = te;  this.t = t; }

        //popup gui, TODO: add code to separate conditions into categories, probably use Attributes
        public override void OnGUI(Rect rect)
        {
            //list all types and select one
            condition = types[EditorGUILayout.Popup("New Condition Type:", types.IndexOf(condition), types.Select(x => x.Name).ToArray())];

            if(GUILayout.Button("Add Condition"))
            {
                te.AddCondition(t, condition);
                editorWindow.Close();
            }
        }
    }
}