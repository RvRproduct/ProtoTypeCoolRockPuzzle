using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (gameObject.tag == "GrappleEnd")
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            StartCoroutine(LerpPlayerToPosition(playerObject, transform.position, 1.0f));
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

}
