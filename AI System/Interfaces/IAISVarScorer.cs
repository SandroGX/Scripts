using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GX.AISystem
{
    public interface IAISVarScorer
    {
        int Score(AISController controller, AISVarSingle var);
        int Score(AISController controller, AISVarList var, int idx);
    }
}