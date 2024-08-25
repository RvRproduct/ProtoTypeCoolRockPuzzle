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

    [SerializeField]
    private float maxAttackCoolDown = 0.5f;
    private float currentAttackCoolDown = 0.5f;

    private PlayerTools playerTools;

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
        playerTools = GetComponent<PlayerTools>();
        playerState = new PlayerState();
        playerState.InitPlayerState();
        OnInstrumentChange(0);
    }

    private void Start()
    {
        PlayerInputManager.OnChangeInstrument += OnInstrumentChange;
        PlayerInputManager.OnPlayerInteract += OnPlayerInteract;
        PlayerInputManager.OnNormalAttack += OnNormalAttack;
        PlayerInputManager.OnSpecialAttack += OnSpecialAttack;
    }

    private void Update()
    {
        if (currentAttackCoolDown < maxAttackCoolDown)
        {
            currentAttackCoolDown += Time.deltaTime;

            if (currentAttackCoolDown >= maxAttackCoolDown)
            {
                resetAttacks();
            }

            if (currentAttackCoolDown > maxAttackCoolDown)
            {
                currentAttackCoolDown = maxAttackCoolDown;
            }
        }
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

    private void OnSpecialAttack()
    {
        if (currentAttackCoolDown >= maxAttackCoolDown)
        {
            currentAttackCoolDown = 0.0f;

            if (audioSource == null) { return; }
            audioSource.Play();

            switch (playerState.CurrnetInstrument)
            {
                case PlayerInstrumentType.Guitar:
                    Debug.Log("Guitar Special");
                    break;
                case PlayerInstrumentType.Drum:
                    Debug.Log("Drum Special");
                    break;
                case PlayerInstrumentType.Keyboard:
                    Debug.Log("KeyBoard Special");
                    break;
                case PlayerInstrumentType.Vocal:
                    Debug.Log("Vocal Special");
                    break;
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
                    Debug.Log("Guitar Normal");
                    break;
                case PlayerInstrumentType.Drum:
                    Debug.Log("Drum Normal");
                    break;
                case PlayerInstrumentType.Keyboard:
                    Debug.Log("KeyBoard Normal");
                    break;
                case PlayerInstrumentType.Vocal:
                    Debug.Log("Vocal Normal");
                    whichNormalAttack(playerTools.vocalNormal);
                    break;
            }
        }
    }

    private void whichNormalAttack(GameObject theAttack)
    {
        GameObject normalAttack = Instantiate(theAttack);
        normalAttack.transform.SetParent(transform);
        normalAttack.transform.localPosition = new Vector3(0, 0, 0);
        normalAttack.transform.localRotation = Quaternion.identity;
        normalAttack.transform.localScale = Vector3.one;
    }

    private void resetAttacks()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Attack"))
            {
                Destroy(child.gameObject);
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
