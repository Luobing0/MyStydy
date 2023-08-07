using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class WaitNode : ActionNode
{
    public float duration = 1f;
    float startTime;

    protected override void OnStart()
    {
        startTime = Time.time;
        body.useGravity = true;
    }

    protected override void OnStop()
    {

    }

    protected override NodeState OnUpdate()
    {
        if (Time.time - startTime > duration )
        {
            return NodeState.Success;
        }
        return NodeState.Running;
    }


}
