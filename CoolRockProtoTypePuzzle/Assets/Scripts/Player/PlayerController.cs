using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerState playerState;
    public PlayerState PlayerState => playerState;

    private void Awake() 
    {
        playerState = new PlayerState();
        playerState.InitPlayerState();
    }
}
