using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrumSpecialWave : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.name);
        if(collider.gameObject.TryGetComponent<DestructibleDrum>(out DestructibleDrum destructibleDrum))
        {
            Debug.Log("DESTORY WALL");
            Destroy(destructibleDrum.gameObject);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        Debug.Log(collider.name);
        if(collider.gameObject.TryGetComponent<DestructibleDrum>(out DestructibleDrum destructibleDrum))
        {
            Debug.Log("DESTORY WALL");
            Destroy(destructibleDrum.gameObject);
        }
    }
}
