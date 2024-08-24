using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerState playerState;
    public PlayerState PlayerState => playerState;
    [SerializeField]
    private MeshRenderer playerMesh;
    private bool isPlayerInteract = false;
    public bool IsPlayerInteract => isPlayerInteract;

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

    private void Awake() 
    {
        playerState = new PlayerState();
        playerState.InitPlayerState();
        OnInstrumentChange(0);
    }

    private void Start()
    {
        PlayerInputManager.OnChangeInstrument += OnInstrumentChange;
        PlayerInputManager.OnPlayerInteract += OnPlayerInteract;
        PlayerInputManager.OnNormalAttack += OnNormalAttack;
    }

    private void OnPlayerInteract(bool value)
    {
        isPlayerInteract = value;
    }

    private void OnInstrumentChange(int value)
    {
        playerState.OnInstrumentChange(value);

        
        switch(playerState.CurrnetInstrument)
        {
            case PlayerInstrumentType.Guitar:   
                SetPlayerColor(Color.red);
                audioSource.clip = guitar;
                Debug.Log("Guitar");
                break;
            case PlayerInstrumentType.Drum:
                SetPlayerColor(Color.green);
                Debug.Log("Drum");
                audioSource.clip = drum;
                break;
            case PlayerInstrumentType.Keyboard:
                SetPlayerColor(Color.blue);
                Debug.Log("KeyBoard");
                audioSource.clip = keyboard;
                break;
            case PlayerInstrumentType.Vocal:
                SetPlayerColor(Color.gray);
                Debug.Log("Vocal");
                audioSource.clip = vocal;
                break;
        }
    }

    private void SetPlayerColor(Color color)
    {
        if(playerMesh == null) { return;}
        playerMesh.material.SetColor("_Color", color);
    }

    private void OnNormalAttack()
    {
        if(audioSource == null) { return; }
        audioSource.Play();
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
