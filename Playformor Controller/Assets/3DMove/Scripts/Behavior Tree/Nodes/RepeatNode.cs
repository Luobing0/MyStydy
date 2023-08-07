using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class RepeatNode : DecoratorNode
{
    protected override void OnStart()
    {
       ;
    }

    protected override void OnStop()
    {

    }

    protected override NodeState OnUpdate()
    {
        childNode.Update();
        return NodeState.Running;
    }

}
