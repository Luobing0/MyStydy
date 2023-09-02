using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public enum NodeState
    {
        Null = 1,//ÎÞÐ§×´Ì¬
        Running,
        Success,
        Failure
    }

    public abstract class BaseNode : ScriptableObject
    {
        public string nodeName;
        [HideInInspector]
        public string guid;
        [HideInInspector]
        public NodeState state = NodeState.Running;
        [HideInInspector]
        public bool started = false;
        [HideInInspector]
        public Vector2 position;

        public Rigidbody body;

        public BlackBorad blackborad;

        public NodeState Update()
        {
            if (!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate();
            if (state == NodeState.Failure || state == NodeState.Success)
            {
                OnStop();
                started = false;
            }

            return state;
        }

        public virtual BaseNode Clone()
        {
            return Instantiate(this);
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract NodeState OnUpdate();


    }

}
