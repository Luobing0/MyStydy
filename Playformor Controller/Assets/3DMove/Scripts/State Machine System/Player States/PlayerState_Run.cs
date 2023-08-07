using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMechine/PlayerState/Run", fileName = "PlayerState_Run")]
public class PlayerState_Run : PlayerState
{
    [SerializeField] float speed;
    [SerializeField] float acceleration = 5f;
    public override void Enter()
    {
        //animator.Play("Run");
        base.Enter();

        currentSpeed = playerController.MoveSpeed;
    }

    public override void LogicUpdate()
    {
        if (!input.IsMove)
        {
            stateMechine.SwichState(typeof(PlayerState_Idle));
        }
        if (input.IsJump)
        {
            stateMechine.SwichState(typeof(PlayerState_JumpUp));
        }
        if (!playerController.IsGround)
        {
            stateMechine.SwichState(typeof(PlayerState_CoyoteTime));
        }
        currentSpeed = Mathf.MoveTowards(currentSpeed, speed, acceleration * Time.deltaTime);
    }

    public override void PhysicUpdate()
    {
        playerController.Move(currentSpeed);
    }
}
