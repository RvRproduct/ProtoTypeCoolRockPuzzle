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

    public delegate void PlayerInteract(bool value);
    public static event PlayerInteract OnPlayerInteract;

    public delegate void NormalAttack();
    public static event NormalAttack OnNormalAttack;

    public delegate void SpecialAttack();
    public static event SpecialAttack OnSpecialAttack;

    public delegate void Pitching(Vector2 value);
    public static event Pitching OnChangePitch;


    //private int 
    private void Awake() 
    {

        playerControl = new PlayerControl();

        playerControl.CharacterControl.Move.started += onMovementInput;
        playerControl.CharacterControl.Move.canceled += onMovementInput;
        playerControl.CharacterControl.Move.performed += onMovementInput;

        playerControl.CharacterControl.ChangeInstrument.started += OnInstrumentInput;

        playerControl.CharacterControl.NormalAttack.started += OnNormalAttackInput;
        playerControl.CharacterControl.SpecialAttack.started += OnSpecialAttackInput;

        playerControl.CharacterControl.Interact.started += OnInteractInput;
        playerControl.CharacterControl.Interact.canceled += OnInteractInput;

        playerControl.CharacterControl.SoloModeDown.started += OnSoloModeDownInput;
        playerControl.CharacterControl.SoloModeRight.started += OnSoloModeRightInput;
        playerControl.CharacterControl.SoloModeLeft.started += OnSoloModeLeftInput;
        playerControl.CharacterControl.SoloModeUp.started += OnSoloModeUpInput;

        playerControl.CharacterControl.Pitching.started += OnPitchingInput;
        playerControl.CharacterControl.Pitching.canceled += OnPitchingInput;
        playerControl.CharacterControl.Pitching.performed += OnPitchingInput;
    }

    private void OnPitchingInput(InputAction.CallbackContext context)
    {
        Vector2 currentInput = context.ReadValue<Vector2>();
        OnChangePitch.Invoke(currentInput);
    }

    private void OnSoloModeUpInput(InputAction.CallbackContext context)
    {
        Debug.Log("Solo Up");
    }

    private void OnSoloModeLeftInput(InputAction.CallbackContext context)
    {
        Debug.Log("Solo Left");
    }

    private void OnSoloModeRightInput(InputAction.CallbackContext context)
    {
        Debug.Log("Solo Right");
    }

    private void OnSoloModeDownInput(InputAction.CallbackContext context)
    {
        Debug.Log("Solo Down");
    }

    private void OnInteractInput(InputAction.CallbackContext context)
    {
        bool currentInput = context.ReadValue<float>() > 0.1f ? true : false;
        //Debug.Log("Interact: " + currentInput);
        OnPlayerInteract.Invoke(currentInput);
    }

    private void OnSpecialAttackInput(InputAction.CallbackContext context)
    {
        OnSpecialAttack.Invoke();
    }

    private void OnNormalAttackInput(InputAction.CallbackContext context)
    {
        OnNormalAttack.Invoke();
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
