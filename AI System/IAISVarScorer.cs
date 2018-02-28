using UnityEngine;
using UnityEditor;

namespace Game.AISystem
{
    public interface IAISVarScorer
    {
        int Score(AISController controller, AISVarSingle var);
        int Score(AISController controller, AISVarList var, int idx);
    }
}