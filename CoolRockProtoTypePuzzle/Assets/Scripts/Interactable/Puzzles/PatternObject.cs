using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternObject : MonoBehaviour 
{
    private bool isActivate = false;
    public bool IsActivate => isActivate;
    [SerializeField]
    private float activateDuration;
    [SerializeField]
    private InteractablePattern interactablePattern;
    private Coroutine activateCoroutine;
    private MeshRenderer meshRenderer;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInputManager.OnNormalAttack += Activate;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            this.player = player;
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            this.player = null;
        }
    }

    private void Activate()
    {
        if(isActivate) { return; }
        if(player == null) { return;}
        isActivate = true;
        interactablePattern.CheckPuzzle();
        activateCoroutine = StartCoroutine(ActivateCountDown());
        if(meshRenderer != null)
        {
            meshRenderer.material.SetColor("_Color", Color.blue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ActivateCountDown()
    {
        yield return new WaitForSeconds(activateDuration);
        isActivate = false;
        if(meshRenderer != null)
        {
            meshRenderer.material.SetColor("_Color", Color.white);
        }
    }

    private void OnDisable() 
    {
        PlayerInputManager.OnNormalAttack -= Activate;
    }
}
