using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    [SerializeField] string stateName;
    [SerializeField, Range(0f, 1f)] float transDuration;

    int stateHash;
    float stateStartTime;

    protected float currentSpeed;
    protected Animator animator;
    protected PlayerStateMechine stateMechine;
    protected PlayerController playerController;
    protected PlayerInput input;

    protected bool IsAnimationFinished => StateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;
    protected float StateDuration => Time.time - stateStartTime;

    public void Initialize(Animator animator, PlayerInput input,PlayerController playerController, PlayerStateMechine stateMechine)
    {
        this.animator = animator;
        this.stateMechine = stateMechine;
        this.input = input;
        this.playerController = playerController;
    }
    private void OnEnable()
    {
        stateHash = Animator.StringToHash(stateName);
    }
    public virtual void Enter()
    {
        stateStartTime = Time.time;
        animator.CrossFade(stateHash, transDuration);
    }

    public virtual void Exit()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicUpdate()
    {
    }
}
