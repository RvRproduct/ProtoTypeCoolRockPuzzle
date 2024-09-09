using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoGrapple : MonoBehaviour
{
    [SerializeField] private GameObject grapplePrefab;
    [SerializeField] private Transform firePoint;
    private float moveFirePoint = 2.0f;
    [SerializeField] private int grapplePieces = 2;
    [SerializeField] private GameObject grappleManager;
    [HideInInspector] public PlayerState playerState;
    private GameObject tempGrapple;

    private void Awake()
    {
        playerState = GetComponentInParent<PlayerController>().PlayerState;
    }

    private void OnEnable()
    {
        StartCoroutine(ShootGrapple());
    }

    private IEnumerator ShootGrapple()
    {
        for (int piece = 0; piece < grapplePieces; piece++)
        {
            GameObject grapple = Instantiate(grapplePrefab, firePoint.position, firePoint.rotation);
            grapple.transform.SetParent(grappleManager.transform, true);
            tempGrapple = grapple;

            Vector3 targetPosition = firePoint.transform.position + (firePoint.transform.forward * moveFirePoint);
            yield return StartCoroutine(LerpGrapplePieceToPosition(grapple, targetPosition, 0.25f));


            if (tempGrapple.tag == "EndGrapple")
            {
                break;
            }
        }
    }

    private IEnumerator LerpGrapplePieceToPosition(GameObject _grapple, Vector3 _targetPosition, float duration)
    {
        Vector3 startingPosition = _grapple.transform.position;
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(startingPosition, _targetPosition, timeElapsed / duration);

            _grapple.transform.position = newPosition;

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        _grapple.transform.position = _targetPosition;
        firePoint.transform.position = _targetPosition;
    }
}
