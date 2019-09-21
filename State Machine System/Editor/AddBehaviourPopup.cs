using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace GX.StateMachineSystem {
    //popup to add behaviour to state
    public class AddBehaviourPopup : PopupWindowContent
    {
        //List of all types inheriting from StateBehaviour
        private static readonly List<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsSubclassOf(typeof(StateBehaviour)) && !x.IsAbstract).ToList();
        //state to add behavoiur
        private readonly StateEditor s;
        //currently selected type
        private Type behaviour = types[0];

        //create popup for state s
        public AddBehaviourPopup(StateEditor s) { this.s = s; }

        //popup gui, TODO: add code to separate behaviour into categories, probably use Attributes
        public override void OnGUI(Rect rect)
        {
            behaviour = types[EditorGUILayout.Popup("New Child Type:", types.IndexOf(behaviour), types.Select(x => x.Name).ToArray())]; //list all types and select one

            if(GUILayout.Button("Add Behaviour"))
            {
                s.AddBehaviour(behaviour);
                editorWindow.Close();
            }
        }
    }
}