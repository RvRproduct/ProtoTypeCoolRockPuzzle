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
    //private int playerHealth;
    
    public void InitPlayerState()
    {
        currentInstrument = PlayerInstrumentType.Guitar;
        playerVolume = 1;
    }

    public void OnInstrumentChange(int valueToAdd)
    {
        int length = Enum.GetNames(typeof(PlayerInstrumentType)).Length;
        int currentValue = (int)currentInstrument;
        currentValue += valueToAdd;
        if(currentValue == length)
        {
            currentValue = 0;
        }
        else if(currentValue < 0)
        {
            currentValue = length - 1;
        }
        currentInstrument = (PlayerInstrumentType)currentValue;
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
