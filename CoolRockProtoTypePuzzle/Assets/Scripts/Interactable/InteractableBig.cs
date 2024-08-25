using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBig : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Big BOy");
        Vector3 bigSize = new Vector3(2, 2, 2);
        int bigVolume = 3;
        Vector3 normalSize = new Vector3(1, 1, 1);
        int normalVolume = 2;

        PlayerState playerState = playerObject.GetComponent<PlayerController>().PlayerState;

        if (playerObject.transform.localScale == normalSize)
        {
            playerState.OnPlayerVolumeChange(bigVolume);
            playerObject.transform.localScale = bigSize;
        }
        else
        {
            playerState.OnPlayerVolumeChange(normalVolume);
            playerObject.transform.localScale = normalSize;
        }
    }
}

