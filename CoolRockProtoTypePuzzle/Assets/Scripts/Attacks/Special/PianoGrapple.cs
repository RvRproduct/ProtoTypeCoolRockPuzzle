using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoGrapple : MonoBehaviour
{
    [SerializeField] private GameObject grapplePrefab;
    [SerializeField] private Transform firePoint;

    private float moveFirePoint = 2.0f;
    [SerializeField] private int grapplePieces = 5;
    [SerializeField] private GameObject grappleManager;
    [HideInInspector] private GrappleManager manager;
    [HideInInspector] public PlayerState playerState;

    private bool hasDestroyedGrapple = false;

    private void Awake()
    {
        manager = grappleManager.GetComponent<GrappleManager>();
        playerState = GetComponentInParent<PlayerController>().PlayerState;
    }

    private void OnEnable()
    {
        manager.playerUsingGrapple = true;
        StartCoroutine(ShootGrapple());
    }

    private void OnDisable()
    {
        hasDestroyedGrapple = false;
    }

    private void Update()
    {
        if (manager.playerReachedDes && !hasDestroyedGrapple)
        {
            manager.playerReachedDes = false;

            manager.DestroyGrapplePieces();
            hasDestroyedGrapple = true;
            
        }
    }

    private IEnumerator ShootGrapple()
    {
        Vector3 tempFirePointPosition = firePoint.position;
        Quaternion tempFirePointRotation = firePoint.rotation;

        for (int piece = 0; piece < grapplePieces; piece++)
        {
            GameObject grapple = Instantiate(grapplePrefab, tempFirePointPosition, tempFirePointRotation);
            grapple.transform.SetParent(grappleManager.transform, true);

            Vector3 targetPosition = tempFirePointPosition + (firePoint.transform.forward * moveFirePoint);
            yield return StartCoroutine(LerpGrapplePieceToPosition(grapple, targetPosition, 0.25f));

            if (piece + 1 == grapplePieces)
            {
                grapple.GetComponentInChildren<Grapple>().lastPieceGrapple = true;
            }

            tempFirePointPosition = targetPosition;

            if (manager.stopEarly)
            {
                break;
            }
        }
    }

    private IEnumerator LerpGrapplePieceToPosition(GameObject _grapple, Vector3 _targetPosition, float _duration)
    {
        Vector3 startingPosition = _grapple.transform.position;

        float timeElapsed = 0;

        while (timeElapsed < _duration)
        {
            if (grappleManager.GetComponent<GrappleManager>().grappleHit)
            {

                manager.stopEarly = true;

                yield break;
            }

            Vector3 newPosition = Vector3.Lerp(startingPosition, _targetPosition, timeElapsed / _duration);

            if (_grapple != null)
            {
                _grapple.transform.position = newPosition;
            }
            

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        if (_grapple != null)
        {
            _grapple.transform.position = _targetPosition;
        }
    }
}
