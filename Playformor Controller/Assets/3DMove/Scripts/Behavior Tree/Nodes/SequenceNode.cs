using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class SequenceNode : CompositeNode
{

    private int current;
    protected override void OnStart()
    {
        current = 0;
    }

    protected override void OnStop()
    {
       
    }

    protected override NodeState OnUpdate()
    {
        var child = childNodes[current];
        switch (child.Update())
        {
            case NodeState.Running:
                return NodeState.Running;
            case NodeState.Success:
            current++;
                break;
            case NodeState.Failure:
                return NodeState.Failure;
        }

        return current == childNodes.Count ? NodeState.Success : NodeState.Running;
    }
}
