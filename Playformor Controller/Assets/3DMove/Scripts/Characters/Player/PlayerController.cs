using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    PlayerGroundDetector groundDetector;
    PlayerInput playerInput;
    Rigidbody rigidBody;


    public AudioSource PlayerSource{get; private set;}


    public bool Victory { get; private set; }
    public bool CanAirJump {get; set;}
    public bool IsGround => groundDetector.IsGround;
    public bool IsFalling => !groundDetector.IsGround && rigidBody.velocity.y < 0f;
    public float MoveSpeed => Mathf.Abs(rigidBody.velocity.x);


    private void Awake()
    {
        groundDetector = GetComponentInChildren<PlayerGroundDetector>();
        playerInput = GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody>();
        PlayerSource = GetComponentInChildren<AudioSource>();
    }

    private void Start()
    {
        playerInput.EnableGamePlayInputs();
    }

    private void Update()
    {
        
    }

    private void OnEnable() {
        EventManager.AddListener( OnLevelPass, EventNames.PlayerPassEvent);
    }

    
    private void OnDisable() {
        EventManager.RemoveListener(OnLevelPass, EventNames.PlayerPassEvent);
    }

    void OnLevelPass() {
        Victory = true;
    }

    public void OnDefead() {
        //Victory = false;
        playerInput.DisablePlayerInputs();
        rigidBody.velocity = Vector3.zero;
        rigidBody.useGravity = false;
        rigidBody.detectCollisions = false;
        GetComponent<StateMechine>().SwichState(typeof(PlayerState_Defeated));
        Debug.Log("Ê§°Ü");
        EventManager.Dispatch(EventNames.UILoseEvent);
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
