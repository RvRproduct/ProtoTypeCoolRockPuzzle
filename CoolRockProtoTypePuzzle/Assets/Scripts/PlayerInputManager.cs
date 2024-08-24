using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    PlayerControl playerControl;

    public delegate void ChangeInstrument(int value);
    public static event ChangeInstrument OnChangeInstrument;

    public delegate void PlayerMovement(Vector2 value);
    public static event PlayerMovement OnPlayerMovement;
    //private int 
    private void Awake() 
    {
        playerControl = new PlayerControl();

        playerControl.CharacterControl.Move.started += onMovementInput;
        playerControl.CharacterControl.Move.canceled += onMovementInput;
        playerControl.CharacterControl.Move.performed += onMovementInput;

        playerControl.CharacterControl.ChangeInstrument.started += OnInstrumentInput;
        
        //normal attack
        //special attack
        //interact
        //solomode
        //pitching
    }

    private void OnInstrumentInput(InputAction.CallbackContext context)
    {
        float currentInput = context.ReadValue<float>();
        Debug.Log("InstrumentChange, + " + currentInput);
        OnChangeInstrument.Invoke((int)currentInput);
    }

    private void onMovementInput(InputAction.CallbackContext context)
    {
        //Debug.Log("MOVE");
        Vector2 currentInput = context.ReadValue<Vector2>();
        OnPlayerMovement.Invoke(currentInput);
    }

    private void OnEnable()
    {
        playerControl.CharacterControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.CharacterControl.Disable();
    }
}
