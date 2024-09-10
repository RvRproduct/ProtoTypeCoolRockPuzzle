using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerState
{
    private PlayerInstrumentType currentInstrument;
    public PlayerInstrumentType CurrnetInstrument => currentInstrument;
    private int playerVolume;
    public int PlayerVolume => playerVolume;

    private Vector2 playerPitch;
    public Vector2 PlayerPitch => playerPitch; 
    private bool playerPitchMode = false;
    public bool PlayerPitchMode => playerPitchMode;

    private bool playerPhaseMode = false;
    public bool PlayerPhaseMode => playerPhaseMode;

    private bool playerGrappleMode = false;
    public bool PlayerGrappleMode => playerGrappleMode;

    private bool playerShootMode = false;
    public bool PlayerShootMode => playerShootMode;
    private bool playerDrumMode = false;
    public bool PlaterDrumMode => playerDrumMode;
    //private int playerHealth;
    
    public void InitPlayerState()
    {
        currentInstrument = PlayerInstrumentType.Guitar;
        playerVolume = 2;
        playerPitch = Vector2.zero;
        playerPitchMode = false;
        playerPhaseMode = false;
        playerShootMode = false;
        playerDrumMode = false;
        playerGrappleMode = false;
    }

    public void OnInstrumentChange(PlayerInstrumentType playerInstrumentType)
    {
        currentInstrument = playerInstrumentType;
        // int length = Enum.GetNames(typeof(PlayerInstrumentType)).Length;
        // int currentValue = (int)currentInstrument;
        // currentValue += valueToAdd;
        // if(currentValue == length)
        // {
        //     currentValue = 0;
        // }
        // else if(currentValue < 0)
        // {
        //     currentValue = length - 1;
        // }
        // currentInstrument = (PlayerInstrumentType)currentValue;
    }

    public void OnPlayerVolumeChange(int newVolume)
    {
        Debug.Log($"Previous Volume {playerVolume}");
        playerVolume = newVolume;
        Debug.Log($"New Volume {playerVolume}");
    }

    public void OnPitchChange(Vector2 updatePitchValue)
    {
        if (playerPitchMode)
        {
            playerPitch = updatePitchValue;
        }

    }

    public void OnPlayerShootActivate(bool activateShoot)
    {
        playerShootMode = activateShoot;
    }

    public void OnPlayerPhaseActivate(bool activatePhase)
    {
        playerPhaseMode = activatePhase;
    }

    public void OnPlayerGrappleActivate(bool activateGrapple)
    {
        playerGrappleMode = activateGrapple;
    }

    public void OnPlayerPitchActivate(bool activatePitch)
    {
        playerPitchMode = activatePitch;
    }

    public void OnPlayerDrumAttackActivate(bool activateDrum)
    {
        playerDrumMode = activateDrum;
    }
}

public enum PlayerInstrumentType
{
    Guitar = 0,
    Drum = 1,
    Keyboard = 2,
    Vocal = 3,
}
