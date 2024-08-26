using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeDestruction : MonoBehaviour
{
    private PlayerState playerState;

    private void Start()
    {
        playerState = GetComponentInParent<PlayerController>().PlayerState;
    }
    

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Volume1") && playerState.PlayerVolume >= 1)
        {
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.CompareTag("Volume2") && playerState.PlayerVolume >= 2)
        {
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.CompareTag("Volume3") && playerState.PlayerVolume >= 3)
        {
            Destroy(collider.gameObject);
        }
    }
}
