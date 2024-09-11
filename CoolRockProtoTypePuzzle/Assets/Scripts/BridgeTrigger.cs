using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject bridge;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<PlayerController>())
        {
            bridge.SetActive(true);
            Destroy(gameObject);
        }
    }
}
