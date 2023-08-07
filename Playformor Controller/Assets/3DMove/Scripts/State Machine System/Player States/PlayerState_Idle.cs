using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMechine/PlayerState/Idle",fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
    [SerializeField] float decleration = 15f;
    public override void Enter()
    {
        //animator.Play("Idle");
        //playerController.SetVelocityX(0f);
        base.Enter();
        currentSpeed = playerController.MoveSpeed;

    }

    public override void LogicUpdate()
    {
        if (input.IsMove)
        {
            stateMechine.SwichState(typeof(PlayerState_Run));
        }
        if (input.IsJump)
        {
            stateMechine.SwichState(typeof(PlayerState_JumpUp));
        }
        if (!playerController.IsGround)
        {
            stateMechine.SwichState(typeof(PlayerState_Fall));
        }
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, decleration * Time.deltaTime);
    }

    public override void PhysicUpdate()
    {
        playerController.SetVelocityX(currentSpeed * playerController.transform.localScale.x);
    }
}
