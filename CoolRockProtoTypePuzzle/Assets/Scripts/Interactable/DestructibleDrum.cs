using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleDrum : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
