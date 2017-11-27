using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Game.SistemaAI
{
    public class AIEditor : EditorWindow
    {
        SAIAI ai;

        Vector2 scrollAcoes = new Vector2();
        Vector2 scrollAcaoEd = new Vector2();

        string novoNome;

        SAIAcao acaoSelecionada;

        bool iniciado = false;
        SAIAI ult;


        int cimaOffset = 20;



        [MenuItem("Window/AI Editor")]
        static void Initialize()
        {
            AIEditor window = GetWindow<AIEditor>();
            window.minSize = new Vector2(1000, 500);
            window.Show();
        }



        void OnEnable()
        {
            ai = Selection.activeObject as SAIAI;

            if (ai)
            {
                Iniciar(ai.root);
                iniciado = true;
                ult = ai;
            }
        }



        void OnGUI()
        {

            GUILayout.BeginVertical();

            ai = (SAIAI)EditorGUILayout.ObjectField("AI:", ai, typeof(SAIAI), false);

            if (ai == null)
            {
                ai = Selection.activeObject as SAIAI;

                GUILayout.BeginHorizontal();

                novoNome = EditorGUILayout.TextField("Novo AI Nome: ", novoNome);

                if (GUILayout.Button("Criar AI"))
                {
                    ai = ScriptableObject.CreateInstance<SAIAI>();
                    ai.name = novoNome;
                    SAIAcao root = ScriptableObject.CreateInstance<SAIAcao>();
                    root.name = "root";
                    root.ai = ai;
                    ai.root = root;
                    Guardar();
                    AssetDatabase.AddObjectToAsset(root, ai);
                    Guardar();
                }

                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.BeginHorizontal();

                Acoes();
                AcaoEditor();

                GUILayout.EndHorizontal();

                if (!iniciado)
                {
                    Iniciar(ai.root);
                    iniciado = true;
                    ult = ai;
                }
                else if (ult != ai) iniciado = false;


            }

            GUILayout.EndVertical();
        }



        void Acoes()
        {
            GUILayout.BeginArea(new Rect(0, cimaOffset, Screen.width / 2, Screen.height));

            GUILayout.BeginScrollView(scrollAcoes);

            if (GUILayout.Button("Guardar"))
                Guardar();

            MostrarAcoes(ai.root);

            GUILayout.EndScrollView();

            GUILayout.EndArea();
        }



        void AcaoEditor()
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2, cimaOffset, Screen.width / 2, Screen.height));

            if (acaoSelecionada == null)
                GUILayout.Label("Selecione uma acao para editar");
            else
            {
                GUILayout.BeginScrollView(scrollAcaoEd);

                acaoSelecionada.OnGui(ai);

                GUILayout.EndScrollView();
            }


            GUILayout.EndArea();
        }



        void Guardar()
        {
            EditorUtility.SetDirty(ai);
            GuardarAcoes(ai.root);

            string AIpath = "Assets/My Assets/AIs/" + ai.name + ".asset";

            SAIAI A = AssetDatabase.LoadAssetAtPath(AIpath, typeof(SAIAI)) as SAIAI;

            if (A == null)
                AssetDatabase.CreateAsset(ai, AIpath);
            else ai = A;
        }



        void GuardarAcoes(SAIAcao root)
        {
            EditorUtility.SetDirty(root);

            foreach (SAIPontuador p in root.pontuadores)
            {
                EditorUtility.SetDirty(p);
            }

            foreach (SAIAcao a in root.filhos)
                GuardarAcoes(a);
        }



        void MostrarAcoes(SAIAcao root)
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();

            GUILayout.Space(15 * root.ordem);

            string n = root.name + ": " + GetMinPontuacao(root) + " - " + GetMaxPontuacao(root);

            if (root.filhos.Count == 0)
                GUILayout.Label(n);
            else
                root.mostrarFilhos = EditorGUILayout.Foldout(root.mostrarFilhos, n);


            if (GUILayout.Button("Select"))
                acaoSelecionada = root;

            if (root.ordem != 0)
            { 
                if (GUILayout.Button("Eliminar"))
                {
                    Eliminar(root);
                }
            }

            GUILayout.EndHorizontal();

            if (root.mostrarFilhos)
            {
                for (int d = 0; d < root.filhos.Count; d++)
                {
                    SAIAcao a = root.filhos[d];
                    MostrarAcoes(a);
                }
            }

            GUILayout.EndVertical();
        }

        void Eliminar(SAIAcao root)
        {
            for (int d = 0; d < root.filhos.Count; d++)
            {
                SAIAcao a = root.filhos[d];
                root.filhos.Remove(a);
                Eliminar(a);
            }

            for (int i = 0; i < root.pontuadores.Count; i++)
            {
                SAIPontuador p = root.pontuadores[i];
                root.pontuadores.Remove(p);
                DestroyImmediate(p, true);
            }

            root.pai.filhos.Remove(root);
            DestroyImmediate(root, true);
        }


        float GetMaxPontuacao(SAIAcao root)
        {
            float p = 0;

            foreach(SAIPontuador o in root.pontuadores)
            {
                p += o.GetMaxPontuacao();
            }

            return p;
        }


        float GetMinPontuacao(SAIAcao root)
        {
            float p = 0;

            foreach (SAIPontuador o in root.pontuadores)
            {
                p += o.GetMinPontuacao();
            }

            return p;
        }


        void Iniciar(SAIAcao root)
        {
            root.ai = ai;

            foreach(SAIPontuador p in root.pontuadores)
            {
                p.ai = ai;
                p.dono = root;
            }

            foreach(SAIAcao a in root.filhos)
            {
                a.pai = root;
                a.ordem = root.ordem + 1;
                Iniciar(a);
            }
        }
    }
}
