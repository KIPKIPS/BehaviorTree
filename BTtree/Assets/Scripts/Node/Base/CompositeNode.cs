using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public abstract class CompositeNode : Node {
        [HideInInspector]public List<Node> children = new List<Node>();
        public override Node Clone() {
            CompositeNode node = Instantiate(this);
            
            node.children = children.ConvertAll(n=>n.Clone());
            return node;
        }
    }
}
