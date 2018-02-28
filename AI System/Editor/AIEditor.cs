using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Game.AISystem
{
    public class AIEditor : EditorWindow
    {
        AISAI ai;

        Vector2 scrollVar = new Vector2();
        Vector2 scrollActions = new Vector2();
        Vector2 scrollSelectedAction = new Vector2();

        string newName;

        AISAction selectedAction;

        bool initialized = false;
        AISAI ult;

        static int topOffset = 20;
        static int horDiv;
        static int height;


        [MenuItem("Window/AI Editor")]
        static void Initialize()
        {
            AIEditor window = GetWindow<AIEditor>();
            window.minSize = new Vector2(800, 500);
            window.Show();
        }


        void OnEnable()
        {
            ai = Selection.activeObject as AISAI;

            if (ai)
            {
                InitializeAI();
                initialized = true;
                ult = ai;
            }
        }


        void OnGUI()
        {

            horDiv = Screen.width / 5;
            height = Screen.height - topOffset;

            GUILayout.BeginVertical();

            ai = (AISAI)EditorGUILayout.ObjectField("AI:", ai, typeof(AISAI), false);

            if (ai == null)
            {
                ai = Selection.activeObject as AISAI;

                GUILayout.BeginHorizontal();

                newName = EditorGUILayout.TextField("Novo AI Nome: ", newName);

                if (GUILayout.Button("Criar AI"))
                {
                    ai = CreateInstance<AISAI>();
                    ai.name = newName;
                    InitializeAI(); initialized = true;
                    Save();
                }

                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.BeginHorizontal();

                Variables();
                Actions();
                ActionEditor();

                GUILayout.EndHorizontal();

                if (!initialized)
                {
                    InitializeAI();
                    initialized = true;
                    ult = ai;
                }
                else if (ult != ai) initialized = false;
            }

            GUILayout.EndVertical();
        }


        void Variables()
        {
            GUILayout.BeginArea(new Rect(0, topOffset, horDiv, height));
            GUILayout.BeginScrollView(scrollVar);

            if (GUILayout.Button("Add Variable")) AddVar(false);

            if (GUILayout.Button("Add Variable List")) AddVar(true);

            for(int i = 0; i < ai.variables.Count; ++i)
            {
                ai.variables[i].OnGui();

                if (GUILayout.Button("Eliminate")) EliminateVar(ai.variables[i]);
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }


        void Actions()
        {
            GUILayout.BeginArea(new Rect(horDiv, topOffset, horDiv * 2, height));
            GUILayout.BeginScrollView(scrollActions);

            if (GUILayout.Button("Save")) Save();

            ShowActions(ai.root);

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }


        void ActionEditor()
        {
            GUILayout.BeginArea(new Rect(horDiv * 3, topOffset, horDiv * 2, height));

            if (selectedAction == null) GUILayout.Label("Select an Action to edit");
            else
            {
                GUILayout.BeginScrollView(scrollSelectedAction);
                selectedAction.OnGui(ai);
                GUILayout.EndScrollView();
            }

            GUILayout.EndArea();
        }


        void AddVar(bool list)
        {
            AISVariable var = list ? (AISVariable)CreateInstance<AISVarList>(): (AISVariable)CreateInstance<AISVarSingle>();
            var.name = "New Var";
            SODatabase.Add(ai, var, ai.variables);
        }


        void ShowActions(AISAction root)
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            
            string n = root.name + ": " + root.GetMinScore() + " - " + root.GetMaxScore();

            if (root.children.Count == 0) EditorGUILayout.LabelField(n);
            else root.showChildren = EditorGUILayout.Foldout(root.showChildren, n);

            if (GUILayout.Button("Select")) selectedAction = root;

            if (root.order != 0)
            {
                if (GUILayout.Button("Eliminate")) EliminateAction(root);
            }

            GUILayout.EndHorizontal();

            if (root.showChildren)
            {
                for (int d = 0; d < root.children.Count; d++)
                {
                    ++EditorGUI.indentLevel;
                    ShowActions(root.children[d]);
                    --EditorGUI.indentLevel;
                }
            }
            GUILayout.EndVertical();
        }


        void Save()
        {
            EditorUtility.SetDirty(ai);
            foreach (AISVariable v in ai.variables) EditorUtility.SetDirty(v);
            SaveActions(ai.root);
        }


        void SaveActions(AISAction root)
        {
            EditorUtility.SetDirty(root);

            foreach (AISScorer p in root.scorers) EditorUtility.SetDirty(p);
            
            foreach (AISAction a in root.children) SaveActions(a);
        }


        void EliminateAction(AISAction root)
        {
            for (int d = 0; d < root.children.Count; d++)
            {
                AISAction a = root.children[d];
                root.children.Remove(a);
                EliminateAction(a);
            }

            for (int i = 0; i < root.scorers.Count; i++)
            {
                AISScorer s = root.scorers[i];
                root.scorers.Remove(s);
                DestroyImmediate(s, true);
            }

            root.parent.children.Remove(root);
            DestroyImmediate(root, true);
        }

        void EliminateVar(AISVariable v)
        {
            ai.variables.Remove(v);
            DestroyImmediate(v, true);
        }


        void InitializeAI()
        {
            
            if(!AssetDatabase.Contains(ai)) AssetDatabase.CreateAsset(ai, "Assets/My Assets/AIs/" + ai.name + ".asset");

            if (ai.root) InitializeActions(ai.root);
            else
            {
                ai.root = CreateInstance<AISAction>();
                ai.root.name = "root";
                ai.root.ai = ai;
                AssetDatabase.AddObjectToAsset(ai.root, ai);
            }
        }

        void InitializeActions(AISAction root)
        {
            root.ai = ai;

            foreach (AISScorer s in root.scorers)
            {
                s.ai = ai;
                s.owner = root;
            }

            foreach (AISAction a in root.children)
            {
                a.parent = root;
                a.order = root.order + 1;
                InitializeActions(a);
            }
        }
    }
}
