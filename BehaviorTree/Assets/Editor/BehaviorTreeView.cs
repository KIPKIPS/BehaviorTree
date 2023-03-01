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
        Insert(0,new GridBackground());
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
        tree.nodes.ForEach(n=> CreateNodeView(n));
    }
    void CreateNodeView(Node node) {
        
    }
}
