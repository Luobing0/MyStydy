using System;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using BehaviorTree;
using UnityEditor;

public class NodeView : Node {
    public Action<NodeView> OnNodeSelected;
    public BaseNode node;
    public Port input;
    public Port output;
    public NodeView(BaseNode baseNode) : base("Assets/Editor/NodeView.uxml") {
        node = baseNode;
        this.title = baseNode.name;
        this.viewDataKey = node.guid;
        style.left = node.position.x;
        style.top = node.position.y;
        InitNodeView(node);
        SetUpClasses();
    }

    private void SetUpClasses() {
        if (node is ActionNode) {
            AddToClassList("action");
        }
        else if (node is CompositeNode) {
            AddToClassList("composite");
        }
        else if (node is DecoratorNode) {
            AddToClassList("decorator");
        }
        else if (node is RootNode) {
            AddToClassList("root");
        }
    }

    public override void SetPosition(Rect newPos) {
        base.SetPosition(newPos);
        Undo.RecordObject(node, "Behavior Tree (Set Position)");
        node.position.x = newPos.x;
        node.position.y = newPos.y;
        EditorUtility.SetDirty(node);
    }



    private void InitNodeView(BaseNode node) {
        if (node is not RootNode)
            input = +this;
        if (!(node is ActionNode)) {
            output = this - (node is DecoratorNode);
        }
        // if (input != null)
        // {
        //     input.style.flexDirection = FlexDirection.Column;
        // }
        // if (output != null)
        // {
        //     output.style.flexDirection = FlexDirection.Column;
        // }

        inputContainer.Add(input);
        outputContainer.Add(output);
    }

    public static Port operator +(NodeView view) {
        Port port = Port.Create<Edge>(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(NodeView));
        return port;
    }
    public static Port operator -(NodeView view, bool isSingle) {
        Port port = Port.Create<Edge>(Orientation.Vertical, Direction.Output,
        isSingle ? Port.Capacity.Single : Port.Capacity.Multi, typeof(NodeView));
        return port;
    }

    public override void OnSelected() {
        base.OnSelected();
        OnNodeSelected?.Invoke(this);
        // this.title = node.name + ": " + node.nodeName;
    }

    public void SortChildren() {
        CompositeNode compositeNode = node as CompositeNode;
        if (compositeNode != null) {
            compositeNode.childNodes.Sort(SortByHorizontalPosition);
        }
    }

    private int SortByHorizontalPosition(BaseNode left, BaseNode right) {
        return left.position.x < right.position.x ? -1 : 1;
    }

    public void UpdateState() {
        RemoveFromClassList("running");
        RemoveFromClassList("success");
        RemoveFromClassList("failure");
        if (Application.isPlaying) {
            switch (node.state) {
                case NodeState.Null:
                    break;
                case NodeState.Running:
                    if (node.started) {
                        AddToClassList("running");
                    }
                    break;
                case NodeState.Success:
                    AddToClassList("success");
                    break;
                case NodeState.Failure:
                    AddToClassList("failure");
                    break;
                default:
                    break;
            }
        }
    }
}
