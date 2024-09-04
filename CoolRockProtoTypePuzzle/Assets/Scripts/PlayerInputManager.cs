using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    PlayerControl playerControl;

    public delegate void ChangeInstrument(PlayerInstrumentType instrumentType);
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

    public delegate void Aiming(bool value);
    public static event Aiming OnAiming;

    public delegate void Phase();
    public static event Phase OnPhase;

    // public delegate void Solo(SoloPatterns soloPattern);
    // public static event Solo OnSoloPress;


    //private int 
    private void Awake() 
    {

        playerControl = new PlayerControl();

        playerControl.CharacterControl.Move.started += onMovementInput;
        playerControl.CharacterControl.Move.canceled += onMovementInput;
        playerControl.CharacterControl.Move.performed += onMovementInput;

        //playerControl.CharacterControl.ChangeInstrument.started += OnInstrumentInput;

        playerControl.CharacterControl.NormalAttack.started += OnNormalAttackInput;
        playerControl.CharacterControl.SpecialAttack.started += OnSpecialAttackInput;

        playerControl.CharacterControl.Interact.started += OnInteractInput;
        playerControl.CharacterControl.Interact.canceled += OnInteractInput;

        playerControl.CharacterControl.Guitar.started += OnGuitarChangeInput;
        playerControl.CharacterControl.Vocal.started += OnVocalChangeInput;
        playerControl.CharacterControl.Drum.started += OnDrumChangeInput;
        playerControl.CharacterControl.Keyboard.started += OnKeyboardChangeInput;

        playerControl.CharacterControl.Pitching.started += OnPitchingInput;
        playerControl.CharacterControl.Pitching.canceled += OnPitchingInput;
        playerControl.CharacterControl.Pitching.performed += OnPitchingInput;

        playerControl.CharacterControl.AimingMode.started += onAimingInput;
        playerControl.CharacterControl.AimingMode.canceled += onAimingInput;
        //playerControl.CharacterControl.AimingMode.started += onAimingInput;

        playerControl.CharacterControl.Phase.started += OnPhaseInput;
    }

    private void OnPhaseInput(InputAction.CallbackContext context)
    {
        OnPhase?.Invoke();
    }

    private void onAimingInput(InputAction.CallbackContext context)
    {
        bool currentInput = context.ReadValue<float>() > 0.1f ? true : false;
        OnAiming?.Invoke(currentInput);
    }

    private void OnPitchingInput(InputAction.CallbackContext context)
    {
        Vector2 currentInput = context.ReadValue<Vector2>();
        OnChangePitch?.Invoke(currentInput);
    }

    private void OnKeyboardChangeInput(InputAction.CallbackContext context)
    {
        //Debug.Log("Solo Up");
        //OnSoloPress?.Invoke(SoloPatterns.up);
        OnChangeInstrument?.Invoke(PlayerInstrumentType.Keyboard);
    }

    private void OnDrumChangeInput(InputAction.CallbackContext context)
    {
        //Debug.Log("Solo Left");
        //OnSoloPress?.Invoke(SoloPatterns.left);
        OnChangeInstrument?.Invoke(PlayerInstrumentType.Drum);
    }

    private void OnVocalChangeInput(InputAction.CallbackContext context)
    {
        //Debug.Log("Solo Right");
        //OnSoloPress?.Invoke(SoloPatterns.right);
        OnChangeInstrument?.Invoke(PlayerInstrumentType.Vocal);
    }

    private void OnGuitarChangeInput(InputAction.CallbackContext context)
    {
        //Debug.Log("Solo Down");
        //OnSoloPress?.Invoke(SoloPatterns.down);
        OnChangeInstrument?.Invoke(PlayerInstrumentType.Guitar);
    }

    private void OnInteractInput(InputAction.CallbackContext context)
    {
        bool currentInput = context.ReadValue<float>() > 0.1f ? true : false;
        //Debug.Log("Interact: " + currentInput);
        OnPlayerInteract?.Invoke(currentInput);
    }

    private void OnSpecialAttackInput(InputAction.CallbackContext context)
    {
        OnSpecialAttack?.Invoke();
    }

    private void OnNormalAttackInput(InputAction.CallbackContext context)
    {
        OnNormalAttack?.Invoke();
    }

    // private void OnInstrumentInput(InputAction.CallbackContext context)
    // {
    //     float currentInput = context.ReadValue<float>();
    //     //Debug.Log("InstrumentChange, + " + currentInput);
    //     OnChangeInstrument?.Invoke((int)currentInput);
    // }

    private void onMovementInput(InputAction.CallbackContext context)
    {
        //Debug.Log("MOVE");
        Vector2 currentInput = context.ReadValue<Vector2>();
        OnPlayerMovement?.Invoke(currentInput);
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
