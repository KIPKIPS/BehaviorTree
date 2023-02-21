using UnityEngine;
using State = AI.Node.State;
namespace AI {
    [CreateAssetMenu()]
    public class BehaviorTree : ScriptableObject {
        public Node rootNode;
        public State treeState = State.Running;
        public State Update() {
            if (rootNode.state == State.Running) {
                treeState = rootNode.Update();
            }
            return treeState;
        }
    }
    
}
