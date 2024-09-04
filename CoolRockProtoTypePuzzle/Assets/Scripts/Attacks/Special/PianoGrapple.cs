using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoGrapple : MonoBehaviour
{
    [SerializeField] private GameObject grapplePrefab;
    [SerializeField] private Transform firePoint;
    private float moveFirePoint = 2.0f;
    [SerializeField] private int grapplePieces = 2;
    [HideInInspector] public PlayerState playerState;

    private void Awake()
    {
        playerState = GetComponentInParent<PlayerController>().PlayerState;
    }

    private void OnEnable()
    {
        playerState.OnPlayerGrappleActivate(!playerState.PlayerGrappleMode);
        ShootGrapple();
    }

    private void ShootGrapple()
    {
        for (int piece = 0; piece < grapplePieces; piece++)
        {
            GameObject grapple = Instantiate(grapplePrefab, firePoint.position, firePoint.rotation);
            firePoint.transform.position += new Vector3(0, 0, moveFirePoint);

            if ((piece + 1) == grapplePieces)
            {
                grapple.tag = "EndGrapple";
            }
            else
            {
                grapple.tag = "PieceGrapple";
            }
        }
    }
}
