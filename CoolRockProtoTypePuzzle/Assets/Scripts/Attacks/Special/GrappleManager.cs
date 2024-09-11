using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleManager : MonoBehaviour
{
    [HideInInspector] public bool playerUsingGrapple = false;
    [HideInInspector] public bool grappleConnected = false;
    [HideInInspector] public bool playerReachedDes = false;
    [HideInInspector] public bool grappleHit = false;
    [HideInInspector] public bool playerNeedsToStop = false;
    [HideInInspector] public bool stopEarly = false;
    [SerializeField]
    private GameObject playerObject;
    private PlayerState playerState;
    private PianoGrapple pianoGrapple;

    private void Start()
    {
        playerState = playerObject.GetComponent<PlayerController>().PlayerState;
        pianoGrapple = playerObject.GetComponentInChildren<PianoGrapple>();
    }


    public void DestroyGrapplePieces()
    {
        playerNeedsToStop = false;
        grappleConnected = false;
        playerReachedDes = false;
        grappleHit = false;
        playerUsingGrapple = false;
        stopEarly = false;

        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in gameObject.transform)
        {
            children.Add(child.gameObject);
        }

        foreach (GameObject child in children)
        {
            Destroy(child);
        }

        playerState.OnPlayerGrappleActivate(!playerState.PlayerGrappleMode);
    }
}
