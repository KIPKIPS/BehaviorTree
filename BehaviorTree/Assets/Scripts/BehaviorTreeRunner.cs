// --[[
//     author:{wkp}
//     time:19:50
// ]]
using System;
using UnityEngine;

namespace AI {
    public class BehaviorTreeRunner : MonoBehaviour {
        private BehaviorTree behaviorTree;
        void Start() {
            behaviorTree = ScriptableObject.CreateInstance<BehaviorTree>();
        }
        private void Update() {
            behaviorTree.Update();
        }
    }
}
