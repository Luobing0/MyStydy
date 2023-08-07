using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMechine/PlayerState/Fall", fileName = "PlayerState_Fall")]
public class PlayerState_Fall : PlayerState
{
    [SerializeField] AnimationCurve speedanimCurve;
    [SerializeField] float moveSpeed = 4f;
    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if (playerController.IsGround)
        {
            stateMechine.SwichState(typeof(PlayerState_Land));
        }
        if (input.IsJump)
        {
            //¶þ¶ÎÌø
            if (playerController.CanAirJump)
            {
                stateMechine.SwichState(typeof(PlayerState_AirJump));
                return;
            }
            input.SetJumpInputBufferTime();

        }
    }

    public override void PhysicUpdate()
    {
        playerController.Move(moveSpeed);
        playerController.SetVelocityY(speedanimCurve.Evaluate(StateDuration));

    }
}
