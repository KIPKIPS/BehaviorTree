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

        public void AddChild(Node parent,Node child) {
            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator) {
                decorator.child = child;
            }
            CompositeNode composite = parent as CompositeNode;
            if (composite) {
                composite.children.Add(child);
            }
        }
        public void RemoveChild(Node parent,Node child) {
            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator) {
                decorator.child = null;
            }
            CompositeNode composite = parent as CompositeNode;
            if (composite) {
                composite.children.Remove(child);
            }
        }
        
        public List<Node> GetChildren(Node parent) {
            CompositeNode composite = parent as CompositeNode;
            if (composite) {
                return composite.children;
            }
            List<Node> children = new List<Node>();
            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator) {
                children.Add(decorator.child);
            }
            return children;
        }
    }
}