
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/StateMechine/PlayerState/Land", fileName = "PlayerState_Land  ")]
public class PlayerState_Land : PlayerState
{
    [Header("Ó²Ö±Ê±¼ä")]
    [SerializeField] float stiffTime = 0.1f;
    public override void Enter()
    {
        base.Enter();
        playerController.SetVelocity(Vector3.zero);
    }

    public override void LogicUpdate()
    {
        if (input.hasJumpInputBuffer || input.IsJump)
        {
            stateMechine.SwichState(typeof(PlayerState_JumpUp));
        }

        if (StateDuration < stiffTime)
        {
            return;
        }

        if (input.IsMove)
        {
            stateMechine.SwichState(typeof(PlayerState_Run));
        }
        if (IsAnimationFinished)
        {
            stateMechine.SwichState(typeof(PlayerState_Idle));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
