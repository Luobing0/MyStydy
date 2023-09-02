using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class DebugLogNode : ActionNode
{

    public string logStr;
    protected override void OnStart()
    {
        //Debug.Log(blackborad.GetData<int>("aa"));
        Debug.Log($"OnStart{logStr}");
    }

    protected override void OnStop()
    {
        //Debug.Log($"OnStop{logStr}");
    }

    protected override NodeState OnUpdate()
    {
        //Debug.Log($"OnUpdate{logStr}");
        return NodeState.Success;
    }
}
