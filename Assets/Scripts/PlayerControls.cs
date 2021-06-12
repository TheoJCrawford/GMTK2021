using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMotor))]
public class PlayerControls : MonoBehaviour
{
    private PlayerInput _input;
    private PlayerMotor _motor;

    private InputAction _moveAction;
    private InputAction _JumpAction;

    public int jumpLimit = 1;

    private int _remainingJump;
    void Start()
    {
        _motor = GetComponent<PlayerMotor>();
        _input = GetComponent<PlayerInput>();
        _moveAction = _input.actions.FindAction("Horizontal Movement");
        _JumpAction = _input.actions.FindAction("Jump");
        ResetJumping();
    }

    // Update is called once per frame
    void Update()
    {
        _moveAction.performed += TakeMoveInput;
        _moveAction.canceled += TakeMoveInput;
        _JumpAction.performed += TakeJumpAction;
        _JumpAction.canceled += TakeJumpAction;
    }
    private void TakeMoveInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.ReadValue<float>() == 0)
        {
            
        }
    }
    private void TakeJumpAction(InputAction.CallbackContext obj)
    {
        
    }

    private void ResetJumping()
    {
        _remainingJump = jumpLimit;
    }
}
