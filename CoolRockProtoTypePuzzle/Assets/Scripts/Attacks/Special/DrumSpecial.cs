using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumSpecial : MonoBehaviour
{
    private PlayerState playerState;
    // Start is called before the first frame update
    private void Awake()
    {
        playerState = GetComponentInParent<PlayerController>().PlayerState;
    }
    
    private void OnEnable()
    {
        playerState.OnPlayerDrumAttackActivate(!playerState.PlayerShootMode);
        DrumAttack();
    }

    private void DrumAttack()
    {
        Debug.Log("Drum Attack");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
