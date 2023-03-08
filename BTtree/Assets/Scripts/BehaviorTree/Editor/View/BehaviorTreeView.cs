using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AI;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using Node = AI.Node;

public class BehaviorTreeView : GraphView {
    public Action<NodeView> OnNodeSelected;
    public new class UxmlFactory : UxmlFactory<BehaviorTreeView, GraphView.UxmlTraits> {
    }

    private BehaviorTree tree;
    public BehaviorTreeView() {
        Insert(0, new GridBackground());
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/BehaviorTree/Editor/BehaviorTreeEditor.uss");
        styleSheets.Add(styleSheet);
    }
    public void PopulateView(BehaviorTree tree) {
        this.tree = tree;
        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if (tree.rootNode == null) {
            tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets();
        }
        //create node view
        tree.nodes.ForEach(n => CreateNodeView(n));
        
        //create edge
        tree.nodes.ForEach(n => {
            var children = tree.GetChildren(n);
            children.ForEach(c => {
                NodeView parentView = FindNodeView(n);
                NodeView childView = FindNodeView(c);
                Edge edge = parentView.output.ConnectTo(childView.input);
                AddElement(edge);
            });
        });
    }
    NodeView FindNodeView(Node node) {
        return GetNodeByGuid(node.guid) as NodeView;
    }
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
        return ports.ToList().Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
    }

    public GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange) {
        if (graphViewChange.elementsToRemove != null) {
            graphViewChange.elementsToRemove.ForEach(elem => {
                NodeView nodeView = elem as NodeView;
                if (nodeView != null) {
                    tree.DeleteNode(nodeView.node);
                }
                
                Edge edge = elem as Edge;
                if (edge != null) {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    tree.RemoveChild(parentView.node,childView.node);
                }
            });
        }
        if (graphViewChange.edgesToCreate != null) {
            graphViewChange.edgesToCreate.ForEach(edge => {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;
                tree.AddChild(parentView.node,childView.node);
            });
        }
        return graphViewChange;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt) {
        foreach (var type in TypeCache.GetTypesDerivedFrom<ActionNode>()) {
            evt.menu.AppendAction($"{type.BaseType.Name}/{type.Name}", a => CreateNode(type));
        }
        foreach (var type in TypeCache.GetTypesDerivedFrom<CompositeNode>()) {
            evt.menu.AppendAction($"{type.BaseType.Name}/{type.Name}", a => CreateNode(type));
        }
        foreach (var type in TypeCache.GetTypesDerivedFrom<DecoratorNode>()) {
            evt.menu.AppendAction($"{type.BaseType.Name}/{type.Name}", a => CreateNode(type));
        }
    }

    void CreateNode(Type type) {
        Node node = tree.CreateNode(type);
        CreateNodeView(node);
    }
    
    void CreateNodeView(Node node) {
        NodeView nodeView = new NodeView(node);
        nodeView.OnNodeSelected += OnNodeSelected; 
        AddElement(nodeView);
    }
}