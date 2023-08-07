using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMechine/PlayerState/CoyoteTime", fileName = "PlayerState_CoyoteTime")]
public class PlayerState_CoyoteTime : PlayerState
{
    [SerializeField] float speed = 5f;
    [SerializeField] float coyoteTime = 0.1f;
    public override void Enter()
    {
        base.Enter();
        //消除玩家的重力
        playerController.SetUseGravity(false);
    }

    public override void Exit()
    {
        playerController.SetUseGravity(true);
    }

    public override void LogicUpdate()
    {
        if (input.IsJump)
        {
            stateMechine.SwichState(typeof(PlayerState_JumpUp));
        }
        if (!input.IsMove || coyoteTime < StateDuration)
        {
            stateMechine.SwichState(typeof(PlayerState_Fall));
        }
    }

    public override void PhysicUpdate()
    {
        playerController.Move(speed);
    }
}
