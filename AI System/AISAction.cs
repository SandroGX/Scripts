using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.AISystem
{
    [System.Serializable]
    public class AISAction : ScriptableObject
    {
        #region Engine
        public AISAI ai;

        public enum Execution { Parallel, Best_Score, Sequential }
        public Execution exe = Execution.Parallel;
        bool init;

        public AISAction parent;
        public List<AISAction> children = new List<AISAction>();
        public List<AISController> controllers = new List<AISController>();
        public Dictionary<AISController, AISAction> bestChild = new Dictionary<AISController, AISAction>();

        public int score;
        public List<AISScorer> scorers = new List<AISScorer>();

        public bool following;
        public System.Action<AISController> follow;
        public AISAction toFollow;
        public Dictionary<AISController, Coroutine> coroutines = new Dictionary<AISController, Coroutine>();
        public float updateTime = 0.25f;


        public virtual void Init()
        {
            if (following) toFollow.follow += OnAction;
            if (exe == Execution.Best_Score) ai.root.follow += ChangeBest;
            init = true;
        }


        public virtual void OnActionEnter(AISController ctrl)
        {
            if (!init) Init();
            
            switch (exe)
            {
                case Execution.Parallel:
                    foreach (AISAction c in children) c.OnActionEnter(ctrl);
                    break;

                case Execution.Best_Score:
                    if (children.Count != 0)
                    {
                        foreach (AISScorer s in children.SelectMany(x => x.scorers)) s.StartScoring(ctrl);
                        bestChild.Add(ctrl, BestAction(ctrl));
                        bestChild[ctrl].OnActionEnter(ctrl);
                    }
                    break;

                case Execution.Sequential:

                    break;
            }

            if(!following)
            coroutines.Add(ctrl, ctrl.StartCoroutine(ActionCoroutine(ctrl)));

            controllers.Add(ctrl);
        }


        public virtual void OnAction(AISController ctrl)
        {
            if (!controllers.Contains(ctrl)) return;

            if (!following && follow != null) follow(ctrl);
        }


        public virtual void OnActionExit(AISController ctrl)
        {
            if (!controllers.Contains(ctrl)) return;

            switch (exe)
            {
                case Execution.Parallel:
                    foreach (AISAction c in children) c.OnActionExit(ctrl);
                    break;

                case Execution.Best_Score:
                    foreach (AISScorer s in children.SelectMany(x => x.scorers)) s.StopScoring(ctrl);
                    if (children.Count != 0)
                    {
                        bestChild[ctrl].OnActionExit(ctrl);
                        bestChild.Remove(ctrl);
                    }
                    break;

                case Execution.Sequential: break;
            }

            if (!following)
            {
                ctrl.StopCoroutine(coroutines[ctrl]);
                coroutines.Remove(ctrl);
            }

            controllers.Remove(ctrl);

            if (controllers.Count == 0) Final();
        }


        public virtual void Final()
        {
            if (following) toFollow.follow -= OnAction;
            if (exe == Execution.Best_Score) ai.root.follow -= ChangeBest;
            init = false;
        }


        public IEnumerator ActionCoroutine(AISController ctrl)
        {
            while (true)
            {
                OnAction(ctrl);
                yield return new WaitForSeconds(updateTime);
            }
        }


        public void ChangeBest(AISController ctrl)
        {
            if (!controllers.Contains(ctrl)) return;

            if (children.Count != 0)
            {
                AISAction best = BestAction(ctrl);
                if (best != bestChild[ctrl])
                {
                    bestChild[ctrl].OnActionExit(ctrl);
                    bestChild[ctrl] = best;
                    bestChild[ctrl].OnActionEnter(ctrl);
                }
            }
        }


        public AISAction BestAction(AISController ctrl)
        {

            AISAction a = null;
            int maxScore = int.MinValue;
            for (int i = 0; i < children.Count; i++)
            {
                int s = children[i].Score(ctrl);

                if (s > maxScore)
                {
                    a = children[i];
                    maxScore = s;
                }
            }
            return a;
        }


        public int Score(AISController ctrl)
        {
            int s = 0;
            for (int i = 0; i < scorers.Count; i++) s += scorers[i].GetScore(ctrl);

            return s;
        }


        public int GetMaxScore()
        {
            int maxScore = 0;

            foreach (AISScorer s in scorers) maxScore += s.GetMaxScore();
            
            return maxScore;
        }
        public int GetMinScore()
        {
            int minScore = 0;

            foreach (AISScorer s in scorers) minScore += s.GetMinScore();
            
            return minScore;
        }


        public List<AISAction> GetAllParents()
        {
            List<AISAction> parents = new List<AISAction>();
            AISAction p = this;

            while ((p = p.parent)) parents.Add(p);
            
            return parents;
        }



        public T Get<T>(AISController ctrl, List<T> tList)
        {
            return tList[controllers.IndexOf(ctrl)];
        }
        #endregion

        #region Editor
#if UNITY_EDITOR

        static bool showScorers;
        public bool showChildren = true;
        static bool showParameters;
        static System.Type newScorerType = AISEditorUtil.allScorers[0];
        static System.Type newChildType = AISEditorUtil.allActions[0];
        public int order = 0;


        public void OnGui(AISAI ai)
        {
            GUILayout.BeginVertical();

            CommonActionParameters();

            if (parent)
            {
                showParameters = EditorGUILayout.Foldout(showParameters, "Action Parameters:");
                if (showParameters)
                {
                    ++EditorGUI.indentLevel;
                    GuiParameters();
                    --EditorGUI.indentLevel;
                }

                showScorers = EditorGUILayout.Foldout(showScorers, "Scorers");
                if (showScorers)
                {
                    ++EditorGUI.indentLevel;
                    ScorerParameters();
                    --EditorGUI.indentLevel;
                }
            }

            GUILayout.EndVertical();
        }


        private void CommonActionParameters()
        {
            name = EditorGUILayout.TextField("Name:", name);
            exe = (Execution)EditorGUILayout.EnumPopup("Execute children in: ", exe);

            FollowOptions();

            CreateChildOptions();
        }

        private void FollowOptions()
        {
            List<AISAction> parentsFollow = GetAllParents().Where(x => !x.following).ToList();
            if (parentsFollow.Count != 0) following = !EditorGUILayout.Toggle("Use own coroutine?", !following);
            else following = false;

            if (following)
            {
                if (!toFollow || !parentsFollow.Contains(toFollow)) toFollow = parentsFollow[0];
                toFollow = parentsFollow[EditorGUILayout.Popup("Coroutine to follow:", parentsFollow.IndexOf(toFollow), parentsFollow.Select(x => x.name).ToArray())];
            }
            else updateTime = EditorGUILayout.FloatField("Update Time:", updateTime);
        }

        private void CreateChildOptions()
        {
            GUILayout.BeginHorizontal();
            newChildType = AISEditorUtil.allActions[EditorGUILayout.Popup("New Child Type:", AISEditorUtil.allActions.IndexOf(newChildType), AISEditorUtil.allActions.Select(x=>x.Name).ToArray())];

            if (GUILayout.Button("Create new child"))
            {
                AISAction child = CreateInstance(newChildType) as AISAction;
                if (child != null)
                {
                    child.name = "New";
                    child.order = order + 1;
                    child.parent = this;
                    child.ai = ai;
                    SODatabase.Add(this, child, children);
                }
            }
            GUILayout.EndHorizontal();
        }


        private void ScorerParameters()
        {
            for (int i = 0; i < scorers.Count; i++)
            {
                AISScorer p = scorers[i];
                p.OnGui();

                if (GUILayout.Button("Eliminate"))
                {
                    scorers.Remove(p);
                    DestroyImmediate(p, true);
                }
            }

            CreateScorerOptions();
        }

        private void CreateScorerOptions()
        {
            newScorerType = AISEditorUtil.allScorers[EditorGUILayout.Popup("New Scorer Type:", AISEditorUtil.allScorers.IndexOf(newScorerType), AISEditorUtil.allScorers.Select(x => x.Name).ToArray())];
            if (GUILayout.Button("Create new Scorer"))
            {
                AISScorer scorer = CreateInstance(newScorerType) as AISScorer;
                if (scorer != null)
                {
                    scorer.name = "New";
                    scorer.owner = this;
                    SODatabase.Add(this, scorer, scorers);
                }
            }
        }


        public virtual void GuiParameters() { }
#endif
#endregion Editor
    }
}
