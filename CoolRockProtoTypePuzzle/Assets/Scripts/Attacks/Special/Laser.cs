using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float laserSpeed = 10f;
    [SerializeField] private float lifeTime = 7f;
    private Vector3 direction;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
        direction = transform.forward;
    }

    private void Update()
    {
        transform.position += direction * laserSpeed * Time.deltaTime;

        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, laserSpeed * Time.deltaTime))
        {
            if (hit.collider.CompareTag("Mirror"))
            {
                RedirectLaserFromMirror(hit.collider.transform);
                hit.collider.gameObject.GetComponent<InteractableMirror>().activatedMirror = true;
                hit.collider.gameObject.GetComponent<InteractableMirror>().ResetCoolDown();
            }
        }
    }

    private void RedirectLaserFromMirror(Transform mirrorTransform)
    {
        Vector3 mirrorForward = mirrorTransform.forward;

        direction = mirrorForward;

        transform.position = mirrorTransform.position;

        transform.rotation = Quaternion.LookRotation(direction);
    }
}
