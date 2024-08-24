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

    private void Awake() 
    {
        playerState = new PlayerState();
        playerState.InitPlayerState();
        OnInstrumentChange(0);
    }

    private void Start()
    {
        PlayerInputManager.OnChangeInstrument += OnInstrumentChange;
    }

    private void OnInstrumentChange(int value)
    {
        playerState.OnInstrumentChange(value);
        //color change
        if(playerMesh == null) { return;}
        switch(playerState.CurrnetInstrument)
        {
            case PlayerInstrumentType.Guitar:   
                playerMesh.material.SetColor("_Color", Color.red);
                break;
            case PlayerInstrumentType.Drum:
                playerMesh.material.SetColor("_Color", Color.green);
                break;
            case PlayerInstrumentType.Keyboard:
                playerMesh.material.SetColor("_Color", Color.blue);
                break;
            case PlayerInstrumentType.Vocal:
                playerMesh.material.SetColor("_Color", Color.gray);
                break;
        }
    }

    private void OnDisable()
    {
        PlayerInputManager.OnChangeInstrument -= OnInstrumentChange;
    }


}
