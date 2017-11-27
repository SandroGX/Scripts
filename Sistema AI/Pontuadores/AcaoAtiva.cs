using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SistemaAI {
    public class AcaoAtiva : SeVerdade
    {

        protected override bool Avaliar(SAIController ai)
        {
            if (dono.pai.exe == SAIAcao.Execucao.Paralelo || dono.pai.filhos.Count == 1)
                return true;
            else if (dono.pai.filhoExe.ContainsKey(ai))
                    return dono.pai.filhoExe[ai] == dono;
                else return false;
        }

        public override float Pontuar(SAIController ai)
        {
            return base.Pontuar(ai);
        }
    }
}
