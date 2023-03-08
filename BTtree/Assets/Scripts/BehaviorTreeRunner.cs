// --[[
//     author:{wkp}
//     time:19:50
// ]]
using System;
using UnityEngine;

namespace AI {
    public class BehaviorTreeRunner : MonoBehaviour {
        public BehaviorTree behaviorTree;
        void Start() {
            behaviorTree = behaviorTree.Clone();
        }
        private void Update() {
            behaviorTree.Update();
        } 
    }
}
