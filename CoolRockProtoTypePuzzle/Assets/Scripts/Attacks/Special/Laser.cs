using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float laserSpeed = 10f;
    [SerializeField] private float lifeTime = 10f;
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
