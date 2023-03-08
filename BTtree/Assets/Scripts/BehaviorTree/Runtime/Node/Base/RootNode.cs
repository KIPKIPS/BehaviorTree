// --[[
//     author:{wkp}
//     time:17:45
// ]]
namespace AI {
    public class RootNode:Node {
        public Node child;
        protected override void OnStart() {
            
        }
        protected override State OnUpdate() {
            return child.Update();
        }
        protected override void OnStop() {
        }

        public override Node Clone() {
            RootNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }
    }
}