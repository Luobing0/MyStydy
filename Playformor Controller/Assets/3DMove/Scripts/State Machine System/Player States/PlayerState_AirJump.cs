using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMechine/PlayerState/AirJump", fileName = "PlayerState_AirJump")]
public class PlayerState_AirJump : PlayerState
{
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] ParticleSystem jumpVFX;
    [SerializeField] AudioClip jumpSFX;
    public override void Enter()
    {
        base.Enter();
        playerController.CanAirJump = false;
        playerController.SetVelocityY(jumpForce);
                playerController.PlayerSource.PlayOneShot(jumpSFX);
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
