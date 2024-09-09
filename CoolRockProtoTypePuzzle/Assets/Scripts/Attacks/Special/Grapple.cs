using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    private GrappleManager manager;

    private void Awake()
    {
        manager = GetComponentInParent<GrappleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!manager.grappleConnected && other.gameObject.tag == "AttachGrapple")
        {
            manager.grappleConnected = true;
            gameObject.tag = "EndGrapple";
            GameObject playerObject = GameObject.FindWithTag("Player");
            StartCoroutine(LerpAndDestroy(playerObject));
        }
        else
        {
            gameObject.tag = "PieceGrapple";
        }

        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator LerpPlayerToPosition(GameObject player, Vector3 targetPosition, float duration)
    {
        Vector3 startingPosition = player.transform.position;
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(startingPosition, targetPosition, timeElapsed / duration);
            Vector3 moveDirection = newPosition - startingPosition;

            player.GetComponent<CharacterController>().Move(moveDirection);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        Vector3 finalMoveDirection = targetPosition - startingPosition;
        player.GetComponent<CharacterController>().Move(finalMoveDirection);
    }

    private IEnumerator LerpAndDestroy(GameObject playerObject)
    {
        yield return StartCoroutine(LerpPlayerToPosition(playerObject, transform.position, 1.0f));

        Destroy(gameObject);
    }

}
