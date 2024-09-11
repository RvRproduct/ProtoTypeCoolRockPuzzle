using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector3 startPoint;
    private int currentCount = 0;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.TryGetComponent<InteractableNpcA>(out InteractableNpcA npcA))
        {
            Destroy(npcA.gameObject);
            currentCount++;
        }
    }

    private void Update()
    {
        if(player.transform.position.y < -1)
        {
            player.transform.position = startPoint;
        }
    }
}
