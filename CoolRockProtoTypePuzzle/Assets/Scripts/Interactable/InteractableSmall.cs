using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSmall : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Small BOy");
        Vector3 smallSize = new Vector3(0.5f, 0.5f, 0.5f);
        int smallVolume = 1;
        Vector3 normalSize = new Vector3(1, 1, 1);
        int normalVolume = 2;

        PlayerState playerState = playerObject.GetComponent<PlayerController>().PlayerState;

        if (playerObject.transform.localScale == normalSize)
        {
            playerState.OnPlayerVolumeChange(smallVolume);
            playerObject.transform.localScale = smallSize;
        }
        else
        {
            playerState.OnPlayerVolumeChange(normalVolume);
            playerObject.transform.localScale = normalSize;
        }
    }
}

