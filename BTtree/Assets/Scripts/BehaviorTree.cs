using System;
using System.Collections.Generic;
using UnityEngine;
using State = AI.Node.State;
using UnityEditor;

namespace AI {
    [CreateAssetMenu()]
    public class BehaviorTree : ScriptableObject {
        public Node rootNode;
        public State treeState = State.Running;
        public List<Node> nodes = new List<Node>();
        public State Update() {
            if (rootNode.state == State.Running) {
                treeState = rootNode.Update();
            }
            return treeState;
        }

        public Node CreateNode(Type type) {
            Node node = CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            nodes.Add(node);
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(Node node) {
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }
    }
}