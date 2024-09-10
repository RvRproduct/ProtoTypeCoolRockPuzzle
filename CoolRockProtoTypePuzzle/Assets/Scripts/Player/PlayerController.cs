using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GrappleManager manager;

    private PlayerState playerState;
    public PlayerState PlayerState => playerState;
    [SerializeField]
    private MeshRenderer playerMesh;
    private bool isPlayerInteract = false;
    public bool IsPlayerInteract => isPlayerInteract;

    [SerializeField]
    private float maxAttackCoolDown = 0.5f;
    private float currentAttackCoolDown = 0.5f;
    private CharacterController characterController;
    [HideInInspector] public Attacks currentAttack;

    [Header("SFX placeholder")]
    [SerializeField]
    private AudioClip guitar;
    [SerializeField]
    private AudioClip drum;
    [SerializeField]
    private AudioClip keyboard;
    [SerializeField]
    private AudioClip vocal;
    [SerializeField]
    private AudioSource audioSource;

    public delegate void NormalAttack(Attacks attacks);
    public static event NormalAttack OnChangeNPCBehavior;

    private void Awake()
    { 
        playerState = new PlayerState();
        playerState.InitPlayerState();
        OnInstrumentChange(0);
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        PlayerInputManager.OnChangeInstrument += OnInstrumentChange;
        PlayerInputManager.OnPlayerInteract += OnPlayerInteract;
        PlayerInputManager.OnNormalAttack += OnNormalAttack;
        PlayerInputManager.OnSpecialAttack += OnSpecialAttack;
        PlayerInputManager.OnChangePitch += OnPitchChange;
    }

    private void Update()
    {
        if (currentAttackCoolDown < maxAttackCoolDown)
        {
            if (playerState.PlayerPitchMode == false && playerState.PlayerGrappleMode == false)
            {
                currentAttackCoolDown += Time.deltaTime;
            }

            if (currentAttackCoolDown >= maxAttackCoolDown)
            {
                resetAttacks(PlayerAttacks.GetAttackName(currentAttack));
            }

            if (currentAttackCoolDown > maxAttackCoolDown)
            {
                currentAttackCoolDown = maxAttackCoolDown;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("AttachGrapple"))
        {
            manager.playerNeedsToStop = true;
        }

        if (hit.gameObject.CompareTag("PieceGrapple"))
        {
            Destroy(hit.gameObject);
        }
    }


    public void SetPosition(Vector3 newPosition)
    {
        if(characterController == null) { return; }
        characterController.enabled = false;
        transform.position = newPosition;
        characterController.enabled = true;
    }

    private void OnPlayerInteract(bool value)
    {
        isPlayerInteract = value;
    }

    private void OnPitchChange(Vector2 value)
    {
        playerState.OnPitchChange(value);
    }

    private void OnInstrumentChange(PlayerInstrumentType playerInstrumentType)
    {
        playerState.OnInstrumentChange(playerInstrumentType);
        
        switch(playerState.CurrnetInstrument)
        {
            case PlayerInstrumentType.Guitar:   
                SetPlayerColor(Color.red);
                audioSource.clip = guitar;
                //Debug.Log("Guitar");
                break;
            case PlayerInstrumentType.Drum:
                SetPlayerColor(Color.green);
                //Debug.Log("Drum");
                audioSource.clip = drum;
                break;
            case PlayerInstrumentType.Keyboard:
                SetPlayerColor(Color.blue);
                //Debug.Log("KeyBoard");
                audioSource.clip = keyboard;
                break;
            case PlayerInstrumentType.Vocal:
                SetPlayerColor(Color.gray);
                //Debug.Log("Vocal");
                audioSource.clip = vocal;
                break;
        }
    }

    private void SetPlayerColor(Color color)
    {
        if(playerMesh == null) { return;}
        playerMesh.material.SetColor("_Color", color);
    }

    private void OnSpecialAttack()
    {
        if (playerState.PlayerPitchMode == true && playerState.PlayerGrappleMode == false)
        {
            currentAttack = Attacks.Pitch;
            OnPitching(PlayerAttacks.GetAttackName(currentAttack));
        }

        if (playerState.PlayerPitchMode == false && playerState.PlayerGrappleMode == true)
        {
            currentAttack = Attacks.Grapple;
        }

        if (currentAttackCoolDown >= maxAttackCoolDown)
        {
            currentAttackCoolDown = 0.0f;
            maxAttackCoolDown = 0.5f;

            if (audioSource == null) { return; }
            audioSource.Play();

            switch (playerState.CurrnetInstrument)
            {
                case PlayerInstrumentType.Guitar:
                    Debug.Log("Guitar Special");
                    currentAttack = Attacks.Laser;
                    break;
                case PlayerInstrumentType.Drum:
                    Debug.Log("Drum Special");
                    currentAttack = Attacks.DrumSpecial;
                    maxAttackCoolDown = 1f;
                    break;
                case PlayerInstrumentType.Keyboard:
                    Debug.Log("KeyBoard Special");
                    currentAttack = Attacks.Grapple;
                    OnGrapple(PlayerAttacks.GetAttackName(currentAttack));
                    break;
                case PlayerInstrumentType.Vocal:
                    Debug.Log("Vocal Special");
                    currentAttack = Attacks.Pitch;
                    OnPitching(PlayerAttacks.GetAttackName(currentAttack));
                    break;
            }

            if (currentAttack != Attacks.None && currentAttack != Attacks.Pitch && currentAttack != Attacks.Grapple)
            {
                whichAttack(PlayerAttacks.GetAttackName(currentAttack));
            }
        }
    }

    private void OnNormalAttack()
    {
        if (currentAttackCoolDown >= maxAttackCoolDown)
        {
            currentAttackCoolDown = 0.0f;

            if (audioSource == null) { return; }
            audioSource.Play();

            switch (playerState.CurrnetInstrument)
            {
                case PlayerInstrumentType.Guitar:
                    //Debug.Log("Guitar Normal");
                    OnChangeNPCBehavior?.Invoke(Attacks.GuitarNormal);
                    //invoke npcbehavior
                    break;
                case PlayerInstrumentType.Drum:
                    //Debug.Log("Drum Normal");
                    OnChangeNPCBehavior?.Invoke(Attacks.DrumNormal);
                    break;
                case PlayerInstrumentType.Keyboard:
                    //Debug.Log("KeyBoard Normal");
                    OnChangeNPCBehavior?.Invoke(Attacks.KeyboardNormal);
                    break;
                case PlayerInstrumentType.Vocal:
                    //Debug.Log("Vocal Normal");
                    OnChangeNPCBehavior?.Invoke(Attacks.Scream);
                    currentAttack = Attacks.Scream;
                    break;
            }

            if (currentAttack != Attacks.None && currentAttack != Attacks.Pitch && currentAttack != Attacks.Grapple)
            {
                whichAttack(PlayerAttacks.GetAttackName(currentAttack));
            }
            
        }
    }

    private void OnPitching(string attackName)
    {
        playerState.OnPlayerPitchActivate(!playerState.PlayerPitchMode);

        foreach (Transform child in transform)
        {
            if (child.CompareTag(attackName))
            {
                child.gameObject.SetActive(playerState.PlayerPitchMode);
                break;
            }
        }

    }

    private void OnGrapple(string attackName)
    {
        playerState.OnPlayerGrappleActivate(!playerState.PlayerGrappleMode);

        foreach (Transform child in transform)
        {
            if (child.CompareTag(attackName))
            {
                child.gameObject.SetActive(playerState.PlayerGrappleMode);
                break;
            }
        }

    }

    private void whichAttack(string attackName)
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag(attackName))
            {
                child.gameObject.SetActive(true);
                break;
            }
        }
    }

    private void resetAttacks(string attackName)
    {
        currentAttack = Attacks.None;

        foreach (Transform child in transform)
        {
            if (child.CompareTag(attackName))
            {
                child.gameObject.SetActive(false);
                break;
            }
        }
    }

    private void OnDisable()
    {
        PlayerInputManager.OnChangeInstrument -= OnInstrumentChange;
    }

    public void ResetInteract()
    {
        isPlayerInteract = false;
    }
}
