using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    private GrappleManager manager;
    [HideInInspector] public bool lastPieceGrapple = false;
    private bool inTheMiddleOfLerpPlayer = false;

    private void Start()
    {
        manager = GetComponentInParent<GrappleManager>();
        gameObject.tag = "PieceGrapple";
    }

    private void Update()
    {
        if (!manager.playerUsingGrapple)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }

        if (!inTheMiddleOfLerpPlayer && lastPieceGrapple)
        {
            manager.DestroyGrapplePieces();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!manager.grappleConnected && other.gameObject.tag == "AttachGrapple")
        {
            manager.grappleConnected = true;
            manager.grappleHit = true;
            gameObject.tag = "EndGrapple";
            GameObject playerObject = GameObject.FindWithTag("Player");
            StartCoroutine(LerpedDes(playerObject));
        }

        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    private IEnumerator LerpPlayerToPosition(GameObject player, Vector3 targetPosition, float duration)
    {
        Vector3 startingPosition = player.transform.position;
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            if (manager.playerNeedsToStop)
            {
                yield break;
            }

            Vector3 newPosition = Vector3.Lerp(startingPosition, targetPosition, timeElapsed / duration);
            Vector3 moveDirection = newPosition - startingPosition;

            player.GetComponent<CharacterController>().Move(moveDirection);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        Vector3 finalMoveDirection = targetPosition - startingPosition;
        player.GetComponent<CharacterController>().Move(finalMoveDirection);
    }

    private IEnumerator LerpedDes(GameObject playerObject)
    {
        inTheMiddleOfLerpPlayer = true;

        yield return StartCoroutine(LerpPlayerToPosition(playerObject, transform.position, 1.0f));

        manager.playerReachedDes = true;
    }

}
