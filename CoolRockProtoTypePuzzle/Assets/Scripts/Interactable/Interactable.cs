using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected MeshRenderer meshRenderer;
    private SphereCollider sphereCollider;
    [HideInInspector] public static GameObject playerObject;
    [SerializeField]
    private float interactRange = 3;
    protected PlayerController player;
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
            sphereCollider.radius = interactRange / gameObject.transform.localScale.x;
            sphereCollider.isTrigger = true;
        }
    }
    protected virtual void OnTriggerEnter(Collider other) 
    {
        if(meshRenderer != null)
        {
            //meshRenderer.material.SetColor("_Color", Color.yellow);
        }
    }
    protected virtual void OnTriggerExit(Collider other) 
    {
        if(meshRenderer != null)
        {
            //meshRenderer.material.SetColor("_Color", Color.white);
        }
        this.player = null;
    }

    protected virtual void OnTriggerStay(Collider other) 
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            this.player = player;
            if(player.IsPlayerInteract)
            {
                playerObject = other.gameObject;
                Interact();
                player.ResetInteract();
            }
        }
    }

    protected virtual void SetColor(Color newColor)
    {
        if(meshRenderer != null)
        {
            meshRenderer.material.SetColor("_Color", newColor);
        }
    }
    protected virtual void Interact(){ }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
