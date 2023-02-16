using UnityEngine;

[CreateAssetMenu()]
public class BehaviorTree : ScriptableObject {
    public Node rootNode;
    public Node.State treeState = Node.State.Running;
    public Node.State Update() {
        return rootNode.Update();
    }
}