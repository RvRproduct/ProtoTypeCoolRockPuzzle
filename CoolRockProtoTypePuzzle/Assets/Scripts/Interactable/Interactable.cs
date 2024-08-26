using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private SphereCollider sphereCollider;
    [HideInInspector] public static GameObject playerObject;
    [SerializeField]
    private float interactRange = 3;
    private void Awake() 
    {
        meshRenderer = GetComponent<MeshRenderer>();
        sphereCollider = GetComponent<SphereCollider>();
        if(sphereCollider != null)
        {
            sphereCollider.radius = interactRange;
            sphereCollider.isTrigger = true;
        }
        else
        {
            sphereCollider = gameObject.AddComponent<SphereCollider>();
            sphereCollider.radius = interactRange;
            sphereCollider.isTrigger = true;
        }
    }
    protected virtual void OnTriggerEnter(Collider other) 
    {
        if(meshRenderer != null)
        {
            meshRenderer.material.SetColor("_Color", Color.yellow);
        }
    }
    protected virtual void OnTriggerExit(Collider other) 
    {
        if(meshRenderer != null)
        {
            meshRenderer.material.SetColor("_Color", Color.white);
        }
    }

    protected virtual void OnTriggerStay(Collider other) 
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            if(player.IsPlayerInteract)
            {
                playerObject = other.gameObject;
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
