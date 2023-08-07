using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMechine/PlayerState/JumpUp", fileName = "PlayerState_JumpUp")]
public class PlayerState_JumpUp : PlayerState
{
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] ParticleSystem jumpVFX;
    public override void Enter()
    {
        base.Enter();
        input.hasJumpInputBuffer = false;
        playerController.SetVelocityY(jumpForce);
        Instantiate(jumpVFX, playerController.transform.position, Quaternion.identity);
    }

    public override void LogicUpdate()
    {
        if (input.IsStopJump || playerController.IsFalling)
        {
            stateMechine.SwichState(typeof(PlayerState_Fall));
        }
    }

    public override void PhysicUpdate()
    {
        playerController.Move(moveSpeed);
    }
}
