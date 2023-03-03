using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using Node = AI.Node;

public class BehaviorTreeView : GraphView {
    public new class UxmlFactory : UxmlFactory<BehaviorTreeView, GraphView.UxmlTraits> {
    }

    private BehaviorTree tree;
    public BehaviorTreeView() {
        Insert(0, new GridBackground());
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviorTreeEditor.uss");
        styleSheets.Add(styleSheet);
    }
    public void PopulateView(BehaviorTree tree) {
        this.tree = tree;
        DeleteElements(graphElements);
        tree.nodes.ForEach(n => CreateNodeView(n));
    }
    void CreateNodeView(Node node) {
        NodeView nodeView = new NodeView(node);
        AddElement(nodeView);
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
}