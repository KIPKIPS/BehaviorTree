using System.Collections.Generic;

namespace AI {
    public abstract class CompositeNode : Node {
        public List<Node> children = new List<Node>();
    }
}
