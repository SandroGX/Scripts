using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.AISystem
{
    public interface IAISObjectScorer<T>
    {
        T ToScore { get; set; }
        AISVarSingle ToScoreVariable { get; set; }

        int CalculateScore(AISController controller);
        int Score(AISController controller, T var);
    }
}