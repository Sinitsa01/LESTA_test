using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private InputActions inputActions;

    private Vector2 inputAxis;

    private bool isJump;

    private void Awake()
    {
        inputActions = new InputActions();

        inputActions.Player.Move.performed += context => OnMove(context);
        inputActions.Player.Jump.performed += context => OnJump();
        inputActions.Player.Jump.canceled += context => JumpEnded();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void OnJump()
    {
        isJump = true;
    }

    private void JumpEnded()
    {
        isJump = false;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        inputAxis = context.ReadValue<Vector2>();
    }

    public Vector2 GetAxis() => inputAxis;

    public bool GetIsJump() => isJump;

}
