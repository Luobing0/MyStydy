using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class JumpNode : ActionNode {
    public float duration = 1f;
    float startTime;
    [SerializeField] LayerMask grondLayer;
    Collider[] colliders = new Collider[1];
    public bool IsGround {
        get {
            bool a = Physics.OverlapSphereNonAlloc(body.transform.position,1f, colliders, grondLayer) != 0;
            Debug.Log(body.transform.position);
            Debug.Log(colliders[0].name);
            return a;
        }
    }

    protected override void OnStart() {
        Debug.Log("ÌøÔ¾");
        body.AddForce(new Vector3(0, 50, 0));
    }

    protected override void OnStop() {

    }

    protected override NodeState OnUpdate() {
        if (IsGround) {
            return NodeState.Success;
        }
        return NodeState.Running;
    }


}
