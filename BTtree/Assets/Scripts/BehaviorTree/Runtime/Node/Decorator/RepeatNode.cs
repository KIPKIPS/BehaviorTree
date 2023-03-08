// --[[
//     author:{wkp}
//     time:19:59
// ]]
namespace AI {
    public class RepeatNode :DecoratorNode {
        protected override void OnStart() {
            
        }
        protected override void OnStop(){
            
        }
        protected override State OnUpdate() {
            child.Update();
            return State.Running;
        }
    }
}