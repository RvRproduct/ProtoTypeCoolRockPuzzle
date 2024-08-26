using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalPitching : MonoBehaviour
{
    private PlayerState playerState;

    private void Awake()
    {
        playerState = GetComponentInParent<PlayerController>().PlayerState;
    }
}
