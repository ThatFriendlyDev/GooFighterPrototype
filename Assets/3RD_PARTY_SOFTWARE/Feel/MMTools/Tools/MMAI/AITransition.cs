using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
    /// <summary>
    /// Transitions are a combination of one or more decisions and destination states whether or not these transitions are true or false. An example of a transition could be "_if an enemy gets in range, transition to the Shooting state_".
    /// </summary>
    [System.Serializable]
    public class AITransition 
    {
 
        public AIDecisionStruct[] Decisions;
        /// the state to transition to if this Decision returns true
        [GUIColor(0.6f, 1f, 0.4f)]
        public string TrueState;
        /// the state to transition to if this Decision returns false
        [GUIColor(1, 0.6f, 0.4f)]
        public string FalseState;
 
    }


    [System.Serializable]
    public struct AIDecisionStruct
    {
        public AIDecision decision;
        public bool result;
    }
}
