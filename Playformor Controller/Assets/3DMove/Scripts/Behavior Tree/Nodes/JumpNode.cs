using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class JumpNode : ActionNode
{
    public float duration = 1f;
    float startTime;

    protected override void OnStart()
    {
        Debug.Log(body);
        body.AddForce(new Vector3(0, 50,0));
    }

    protected override void OnStop()
    {

    }

    protected override NodeState OnUpdate()
    {
        if (Time.time - startTime > duration)
        {
            return NodeState.Success;
        }
        return NodeState.Running;
    }


}
