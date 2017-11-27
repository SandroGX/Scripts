using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.SistemaAI
{
    [System.Serializable]
    public class SAIAcao : ScriptableObject
    {
#if UNITY_EDITOR

        static List<string> todasAcoes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsSubclassOf(typeof(SAIAcao)) || x == typeof(SAIAcao)).Select(x => x.ToString()).ToList();
        static List<string> todosPontuadores = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsSubclassOf(typeof(SAIPontuador)) || x == typeof(SAIPontuador)).Select(x => x.ToString()).ToList();

        static bool mostrarPontuadores;
        public bool mostrarFilhos;
        static string novoPontuador = todosPontuadores[0];
        static string novoFilhoTipo = todasAcoes[0];
        public int ordem = 0;

#endif

        public SAIAI ai;

        public enum Execucao { Paralelo, Mais_Forte, Consecutivo}
        public Execucao exe = Execucao.Paralelo;

        public SAIAcao pai;
        public List<SAIAcao> filhos = new List<SAIAcao>();
        public Dictionary<SAIController, SAIAcao> filhoExe = new Dictionary<SAIController, SAIAcao>();

        public float pontuacao;
        public List<SAIPontuador> pontuadores = new List<SAIPontuador>();

        public Dictionary<SAIController, Coroutine> coroutines = new Dictionary<SAIController, Coroutine>();
        public float intervalo = 0.25f;


        public virtual void OnActionEnter(SAIController controller)
        {

            switch (exe)
            {
                case Execucao.Paralelo:

                    foreach (SAIAcao c in filhos)
                        c.OnActionEnter(controller);

                    break;


                case Execucao.Mais_Forte:

                    if (filhos.Count != 0)
                    {
                        filhoExe.Add(controller, MelhorAcao(controller));
                        filhoExe[controller].OnActionEnter(controller);
                    }
  
                    break;


                case Execucao.Consecutivo:

                    break;
            }

            coroutines.Add(controller, controller.StartCoroutine(ActionCoroutine(controller)));

        }


        public virtual void OnAction(SAIController controller)
        {
            switch (exe)
            {

                case Execucao.Mais_Forte:

                    if (filhos.Count != 0)
                    {
                        SAIAcao a = MelhorAcao(controller);

                        if (a != filhoExe[controller])
                        {
                            //if (filhoExe[controller] != null)
                                filhoExe[controller].OnActionExit(controller);

                            filhoExe[controller] = a;
                            filhoExe[controller].OnActionEnter(controller);
                        }
                    }

                    break;


                case Execucao.Consecutivo:

                    break;
            }

        }


        public virtual void OnActionExit(SAIController controller)
        {
           
            switch (exe)
            {
                case Execucao.Paralelo:

                    foreach (SAIAcao c in filhos)
                        c.OnActionExit(controller);

                    break;


                case Execucao.Mais_Forte:

                    if (filhos.Count != 0)
                    {
                        filhoExe[controller].OnActionExit(controller);
                        filhoExe.Remove(controller);
                    }

                    break;


                case Execucao.Consecutivo:

                    break;
            }

            controller.StopCoroutine(coroutines[controller]);
            coroutines.Remove(controller);

        }


        public IEnumerator ActionCoroutine(SAIController controller)
        {
            while (true)
            {
                OnAction(controller);
                yield return new WaitForSeconds(intervalo);
            }
        }


        public float Pontuar(SAIController controller)
        {
            float p = 0;

            for(int i = 0; i < pontuadores.Count; i++)
            {
                p += pontuadores[i].Pontuar(controller);
            }

            return p;
        }



        public SAIAcao MelhorAcao(SAIController controller)
        {

            SAIAcao a = null;

            float pontuacaoMax = float.MinValue;

            for (int i = 0; i < filhos.Count; i++)
            {
                float p = filhos[i].Pontuar(controller);

                //Debug.Log(filhos[i].name + ": " + p);

                if (p > pontuacaoMax)
                {
                    a = filhos[i];
                    pontuacaoMax = p;
                }

            }

            return a;
        }
        

#if UNITY_EDITOR
        public void OnGui(SAIAI ai)
        {

            GUILayout.BeginVertical();

            name = EditorGUILayout.TextField("Nome: ", name);

            exe = (Execucao)EditorGUILayout.EnumPopup("Executar filhos em: ", exe);

            if(pai)
                GUILayout.Label(pai.name);

            intervalo = EditorGUILayout.FloatField("Tempo para update: ", intervalo);

            GuiParametros();

            novoFilhoTipo = todasAcoes[EditorGUILayout.Popup("Novo Filho Tipo: ", todasAcoes.IndexOf(novoFilhoTipo), todasAcoes.ToArray())];

            if (GUILayout.Button("Criar nova Acao filha"))
            {
                SAIAcao a = ScriptableObject.CreateInstance(novoFilhoTipo) as SAIAcao;

                if (a != null)
                {
                    a.name = "Novo";
                    a.ordem = ordem + 1;
                    filhos.Add(a);
                    a.pai = this;
                    AssetDatabase.AddObjectToAsset(a, ai);
                }
            }

            mostrarPontuadores = EditorGUILayout.Foldout(mostrarPontuadores, "Pontuadores");

            if (mostrarPontuadores)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Space(15);

                GUILayout.BeginVertical();


                for (int i = 0; i < pontuadores.Count; i++)
                {
                    SAIPontuador p = pontuadores[i];

                    p.OnGui();

                    if (GUILayout.Button("Eliminar"))
                    {
                        pontuadores.Remove(p);
                        DestroyImmediate(p, true);
                    }
                }


                novoPontuador = todosPontuadores[EditorGUILayout.Popup("Novo Pontuador Tipo: ", todosPontuadores.IndexOf(novoPontuador), todosPontuadores.ToArray())];

                if (GUILayout.Button("Criar novo pontuador"))
                {
                    SAIPontuador p = ScriptableObject.CreateInstance(novoPontuador) as SAIPontuador;

                    if (p != null)
                    {
                        p.name = "Novo";
                        p.dono = this;
                        pontuadores.Add(p);
                        AssetDatabase.AddObjectToAsset(p, ai);
                    }
                }

                GUILayout.EndVertical();

                GUILayout.EndHorizontal();

            }


            GUILayout.EndVertical();
        }


        public virtual void GuiParametros()
        {

        }
#endif

    }
}
