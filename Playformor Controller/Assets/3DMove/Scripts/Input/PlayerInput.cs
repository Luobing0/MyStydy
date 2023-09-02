using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    @PlyaerInputActions playerInputActions;

    [SerializeField]
    float jumpInputBufferTime = 0.5f;
    WaitForSeconds waitJumpInputBufferTime;

    Vector2 axes => playerInputActions.GamePlay.Axes.ReadValue<Vector2>();
    public bool hasJumpInputBuffer { get; set; }
    public bool IsJump => playerInputActions.GamePlay.Jump.WasPressedThisFrame();
    public bool IsStopJump => playerInputActions.GamePlay.Jump.WasReleasedThisFrame();
    public bool IsMove => AxesX != 0;
    public float AxesX => axes.x;
    private void Awake()
    {
        playerInputActions = new PlyaerInputActions();
        waitJumpInputBufferTime = new WaitForSeconds(jumpInputBufferTime);
    }

    private void OnEnable()
    {
        playerInputActions.GamePlay.Jump.canceled += delegate
        {
            hasJumpInputBuffer = false;
        };
    }
    public void EnableGamePlayInputs()
    {
        playerInputActions.GamePlay.Enable();
        //项目不会用到点击，将光标设置为锁定模式
        Cursor.lockState = CursorLockMode.Locked;
    }

    //private void OnGUI()
    //{
    //    Rect rect = new Rect(200, 200, 200, 200);
    //    string message = "Has Jump Input Buffer:" + hasJumpInputBuffer;
    //    GUIStyle style = new GUIStyle();
    //    style.fontSize = 20;
    //    style.fontStyle = FontStyle.Bold;
    //    GUI.Label(rect, message, style);
    //}

    public void DisablePlayerInputs() {
        playerInputActions.GamePlay.Disable();
        Cursor.lockState = CursorLockMode.None;
    }

    public void SetJumpInputBufferTime()
    {
        StopCoroutine(nameof(JumpInputBufferCoroutine));
        StartCoroutine(nameof(JumpInputBufferCoroutine));
    }

    IEnumerator JumpInputBufferCoroutine()
    {
        hasJumpInputBuffer = true;
        yield return waitJumpInputBufferTime;
        hasJumpInputBuffer = false;
    }
}
