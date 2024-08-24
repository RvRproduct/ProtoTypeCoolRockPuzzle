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
    //private int playerHealth;
    
    public void InitPlayerState()
    {
        currentInstrument = PlayerInstrumentType.Guitar;
        playerVolume = 1;
    }

    public void OnInstrumentChange(PlayerInstrumentType playerInstrumentType)
    {
        currentInstrument = playerInstrumentType;
    }

    public void OnPlayerVolumeChange(int newVolume)
    {
        playerVolume = newVolume;
    }
}

public enum PlayerInstrumentType
{
    Guitar = 0,
    Drum = 1,
    Keyboard = 2,
    Vocal = 3,
}
