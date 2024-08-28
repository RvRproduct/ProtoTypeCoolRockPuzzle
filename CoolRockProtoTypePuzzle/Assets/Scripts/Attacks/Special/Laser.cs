using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 200f;
    [SerializeField] private float lifeTime = 10f;
    private Vector3 direction = Vector3.forward;
    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
        direction = transform.forward;
    }

    private void Update()
    {
        rb.AddForce(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");

        if (collision.gameObject.CompareTag("Mirror"))
        {

            Debug.Log("Hit");
            Vector3 normal = collision.contacts[0].normal;

            direction = Vector3.Reflect(direction, normal);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
