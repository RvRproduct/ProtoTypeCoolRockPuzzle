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
    //private int 
    private void Awake() 
    {
        playerControl = new PlayerControl();

        playerControl.CharacterControl.Move.started += onMovementInput;
        playerControl.CharacterControl.Move.canceled += onMovementInput;
        playerControl.CharacterControl.Move.performed += onMovementInput;

        playerControl.CharacterControl.ChangeInstrument.started += OnInstrumentInput;
    }

    private void OnInstrumentInput(InputAction.CallbackContext context)
    {
        float currentInput = context.ReadValue<float>();
        Debug.Log("InstrumentChange, + " + currentInput);
        OnChangeInstrument.Invoke((int)currentInput);
    }

    private void onMovementInput(InputAction.CallbackContext context)
    {
        Debug.Log("MOVE");
        // currentMovementInput = context.ReadValue<Vector2>();
        // currentMovement.x = currentMovementInput.x;
        // currentMovement.z = currentMovementInput.y;
        // isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
