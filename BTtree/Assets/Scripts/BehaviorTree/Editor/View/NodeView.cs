// --[[
//     author:{wkp}
//     time:16:57
// ]]
using System;
using AI;
using UnityEngine;
using UnityEditor;
using Node = AI.Node;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class NodeView : UnityEditor.Experimental.GraphView.Node {

    public Action<NodeView> OnNodeSelected;
    public Node node;
    public Port input;
    public Port output;
    
    public NodeView(Node node) {
        this.node = node;
        this.title = node.name;
        this.viewDataKey = node.guid;
        style.left = node.position.x;
        style.top = node.position.y;

        CreateInputPorts();
        CreateOutputPorts();
    }
    public void CreateInputPorts() {
        switch (node) {
            case ActionNode:
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case CompositeNode:
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case DecoratorNode:
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case RootNode:
                break;
        }
        if (!(input is null)) {
            input.portName = "";
            inputContainer.Add(input);
        }
    }

    public void CreateOutputPorts() {
        switch (node) {
            case ActionNode:
                break;
            case CompositeNode:
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
                break;
            case DecoratorNode:
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
                break;
            case RootNode:
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
                break;
        }
        if (!(output is null)) {
            output.portName = "";
            outputContainer.Add(output);
        }
    }
    public override void SetPosition(Rect newPos) {
        base.SetPosition(newPos);
        node.position.x = newPos.xMin;
        node.position.y = newPos.yMin;
    }

    public override void OnSelected() {
        base.OnSelected();
        OnNodeSelected?.Invoke(this);
    }
}
