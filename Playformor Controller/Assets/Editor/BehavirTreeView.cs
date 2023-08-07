using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using BehaviorTree;

public class BehavirTreeView : GraphView
{
    public Action<NodeView> OnNodeSelected;
    MyBehaviorTree tree;
    public new class UxmlFactory : UxmlFactory<BehavirTreeView, UxmlTraits> { }

    public BehavirTreeView()
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehavirWindow.uss"));
        // GraphViewMenu();
        Undo.undoRedoPerformed += OnUndoRedo;
    }

    private void OnUndoRedo()
    {
        if (tree != null)
            PopulateView(tree);
        AssetDatabase.SaveAssets();
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        // base.BuildContextualMenu(evt);
        // evt.menu.AppendAction("创建节点", CreateNode);
        {
            var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
            }
        }
        {
            var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
            }
        }
        {
            var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
            }
        }
    }

    internal void PopulateView(MyBehaviorTree tree)
    {
        this.tree = tree;
        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if (tree.rootNode == null)
        {
            tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets();
        }

        //创建节点视图
        tree.nodes.ForEach(n => CreateNodeView(n));
        //创建连线
        tree.nodes.ForEach(n =>
        {
            var children = tree.GetNodes(n);
            children.ForEach(c =>
            {
                NodeView parentView = FindNodeView(n);
                NodeView childView = FindNodeView(c);
                Edge edge = parentView.output.ConnectTo(childView.input);
                AddElement(edge);
            });
        });
    }
    NodeView FindNodeView(BaseNode node)
    {
        return GetNodeByGuid(node.guid) as NodeView;
    }
    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        graphViewChange.elementsToRemove?.ForEach(elem =>
            {
                if (elem is NodeView nodeView)
                {
                    tree.DeletNode(nodeView.node);
                }
                else if (elem is Edge edge)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    tree.RemoveChildNode(parentView.node, childView.node);
                }

            });

        graphViewChange.edgesToCreate?.ForEach(edge =>
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;
                tree.AddChildNode(parentView.node, childView.node);
            });
        if(graphViewChange.movedElements != null){
            nodes.ForEach((n) =>{
                NodeView nodeView = n as NodeView;
                nodeView.SortChildren();
            });
        }
        return graphViewChange;
    }
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        Debug.Log(startPort);
        return ports.Where(endPorts => endPorts.direction != startPort.direction && endPorts.node != startPort.node).ToList();
    }
    void CreateNode(Type type)
    {
        BaseNode node = tree.CreateNode(type);
        CreateNodeView(node);
    }
    void CreateNodeView(BaseNode node)
    {
        NodeView nodeView = new NodeView(node);
        nodeView.OnNodeSelected = OnNodeSelected;
        AddElement(nodeView);
    }
}
