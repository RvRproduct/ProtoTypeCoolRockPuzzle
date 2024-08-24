using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private SphereCollider sphereCollider;
    [SerializeField]
    private float interactRange = 3;
    private void Awake() 
    {
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
        if(sphereCollider != null)
        {
            sphereCollider.radius = interactRange;
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(meshRenderer != null)
        {
            meshRenderer.material.SetColor("_Color", Color.yellow);
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(meshRenderer != null)
        {
            meshRenderer.material.SetColor("_Color", Color.white);
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            if(player.IsPlayerInteract)
            {
                Interact();
                player.ResetInteract();
            }
        }
    }
    protected virtual void Interact(){ }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
