using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    PlayerGroundDetector groundDetector;
    PlayerInput playerInput;
    Rigidbody rigidBody;

    public bool CanAirJump {get; set;}
    public bool IsGround => groundDetector.IsGround;
    public bool IsFalling => !groundDetector.IsGround && rigidBody.velocity.y < 0f;
    public float MoveSpeed => Mathf.Abs(rigidBody.velocity.x);


    private void Awake()
    {
        groundDetector = GetComponentInChildren<PlayerGroundDetector>();
        playerInput = GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        playerInput.EnableGamePlayInputs();
    }

    private void Update()
    {

    }

    public void Move(float velocityX)
    {
        if (playerInput.IsMove)
        {
            transform.localScale = new Vector3(playerInput.AxesX, 1f, 1f);
        }
        SetVelocityX(velocityX * playerInput.AxesX);
    }

    public void SetVelocity(Vector3 velocity)
    {
        rigidBody.velocity = velocity;
    }

    public void SetVelocityX(float velocityX)
    {
        rigidBody.velocity = new Vector3(velocityX, rigidBody.velocity.y);
    }

    public void SetVelocityY(float velocityY)
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, velocityY);
    }

    public void SetUseGravity(bool value)
    {
        rigidBody.useGravity = value;
    }
}
