using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
    [System.Serializable]
    public class AIActionsList : MMReorderableArray<AIAction>
    {
    }
    [System.Serializable]
    public class AITransitionsList : MMReorderableArray<AITransition>
    {
    }

    /// <summary>
    /// A State is a combination of one or more actions, and one or more transitions. An example of a state could be "_patrolling until an enemy gets in range_".
    /// </summary>
    [System.Serializable]
    public class AIState 
    {
        /// the name of the state (will be used as a reference in Transitions
        [GUIColor(0.6f, 0.6f, 1f)]
        [PreviewField]
        [TableColumnWidth(20, true)]
        public string StateName;
         
        [TableColumnWidth(200, true)]
        public List<AIAction> Actions; 
        [TableColumnWidth(500, true)]
        public List<AITransition> Transitions;/*

        /// a list of actions to perform in this state
        public List<AIAction> Actions;
        /// a list of transitions to evaluate to exit this state
        public List<AITransition> Transitions;*/

        protected AIBrain _brain;

        /// <summary>
        /// Sets this state's brain to the one specified in parameters
        /// </summary>
        /// <param name="brain"></param>
        public virtual void SetBrain(AIBrain brain)
        {
            _brain = brain;
        }
                	
        /// <summary>
        /// On enter state we pass that info to our actions and decisions
        /// </summary>
        public virtual void EnterState()
        {
            foreach (AIAction action in Actions)
            {
                action.OnEnterState();
            }
            foreach (AITransition transition in Transitions)
            {
                if (transition.Decisions != null)
                {
					foreach (var decisionStruct in transition.Decisions)
                    {
                        decisionStruct.decision.OnEnterState();
                    }
                }
            }
        }

        /// <summary>
        /// On exit state we pass that info to our actions and decisions
        /// </summary>
        public virtual void ExitState()
        {
            foreach (AIAction action in Actions)
            {
                action.OnExitState();
            }
            foreach (AITransition transition in Transitions)
            {
                if (transition.Decisions != null)
                {
                    foreach (var decisionStruct in transition.Decisions)
                    {
                        decisionStruct.decision.OnExitState();
                    }
                }
            }
        }

        /// <summary>
        /// Performs this state's actions
        /// </summary>
        public virtual void PerformActions()
        {
            if (Actions.Count == 0) { return; }
            for (int i=0; i<Actions.Count; i++) 
            {
                if (Actions[i] != null)
                {
                    Actions[i].PerformAction();
                }
                else
                {
                    Debug.LogError("An action in " + _brain.gameObject.name + " is null.");
                }
            }
        }

        /// <summary>
        /// Tests this state's transitions
        /// </summary>
        public virtual void EvaluateTransitions()
        {
            if (Transitions.Count == 0) { return; }
            for (int i = 0; i < Transitions.Count; i++) 
            {
                if (Transitions[i].Decisions != null)
                {
                    bool transitionToTrueState = true;
                    List<bool> decisionResultsList = new List<bool>();
					foreach(var decisionStruct in Transitions[i].Decisions)
					{   
                        
                        var decisionResult = decisionStruct.decision.Decide();
                        if (!decisionResult)
						{
                            transitionToTrueState = false;
                        }

                        decisionResultsList.Add(decisionResult);
                    }

					for (int j = 0; j < Transitions[i].Decisions.Length; j++)
					{
                        Transitions[i].Decisions[j].result = decisionResultsList[j];
                    }
 
                    if (transitionToTrueState)
                    {
                        if (!string.IsNullOrEmpty(Transitions[i].TrueState))
                        {
                            _brain.TransitionToState(Transitions[i].TrueState);
                            break;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Transitions[i].FalseState))
                        {
                            _brain.TransitionToState(Transitions[i].FalseState);
                            break;
                        }
                    }
                }                
            }
        }        
	}
}
