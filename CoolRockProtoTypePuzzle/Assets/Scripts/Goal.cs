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
    private Vector3 originPosition;
    private int currentCount = 0;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.TryGetComponent<InteractableNpcA>(out InteractableNpcA npcA))
        {
            Destroy(npcA.gameObject);
            currentCount++;
            if(currentCount >= 5)
            {
                Debug.Log("YOU ARE A SUPER STAR");
            }
        }
    }

    private void Start()
    {
        originPosition = player.transform.position;
    }

    private void Update()
    {
        if(player.transform.position.y < -1)
        {
            player.transform.position = originPosition;
        }
    }
}
