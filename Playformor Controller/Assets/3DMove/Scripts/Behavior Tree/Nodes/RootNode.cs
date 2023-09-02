using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class RootNode : BaseNode
{
    public BaseNode childNode;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {

    }

    public override BaseNode Clone()
    {
        RootNode node = Instantiate(this);
        node.childNode = childNode.Clone();
        return node;
    }

    protected override NodeState OnUpdate()
    {
        return childNode.Update();
    }
}
