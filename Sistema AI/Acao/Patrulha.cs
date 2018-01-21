using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SistemaAI
{
    [System.Serializable]
    public class Patrulha : MudarEstado
    {

        int pontoAtual;
        SAIPontosDePatrulha pontos;

        [SerializeField]
        float distMudar;

        public override void OnActionEnter(SAIController controller)
        {

            pontos = controller.GetComponent<SAIPontosDePatrulha>();

            if (pontos == null) return;

            float disMin = float.MaxValue;

            for(int i = 0; i < pontos.pontosDePatrulha.Count; i++)
            {
                float dis = Vector3.Distance(controller.transform.position, pontos.pontosDePatrulha[i].position);

                if (dis < disMin)
                {
                    disMin = dis;
                    pontoAtual = i;
                }
            }

            controller.navAgent.destination = pontos.pontosDePatrulha[pontoAtual].position;

            base.OnActionEnter(controller);
        }


        public override void OnAction(SAIController controller)
        {
            if (pontos == null) return;

            if (Vector3.Distance(controller.transform.position, pontos.pontosDePatrulha[pontoAtual].position) < distMudar)
            {
                pontoAtual++;

                if (pontoAtual > pontos.pontosDePatrulha.Count - 1)
                    pontoAtual = 0;

                controller.navAgent.destination = pontos.pontosDePatrulha[pontoAtual].position;
            }

            base.OnAction(controller);
        }


#if UNITY_EDITOR
        public override void GuiParametros()
        {
            GUILayout.BeginVertical();

            distMudar = UnityEditor.EditorGUILayout.FloatField("Distancia para mudar ponto: ", distMudar);

            base.GuiParametros();

            GUILayout.EndVertical();
        }
#endif
    }
}
