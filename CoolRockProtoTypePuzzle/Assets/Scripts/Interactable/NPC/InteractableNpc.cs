using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableNpc : Interactable
{
    protected PlayerController currentPlayer;
    protected Attacks currentAttackBehaviour;
    
    private void Start()
    {
        PlayerController.OnChangeNPCBehavior += OnBehaviorChange;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            currentPlayer = player;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            currentPlayer = player;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if(other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            currentPlayer = null;
        }
    }

    protected virtual void Update()
    {
        switch(currentAttackBehaviour)
        {
            case Attacks.GuitarNormal:
                DoGuitarNormalBehavior();
                break;
            case Attacks.DrumNormal:
                DoDrumNormalBehavior();
                break;
            case Attacks.KeyboardNormal:
                DoKeyboardNormalBehavior();
                break;
            case Attacks.Scream:
                DoVocalNormalBehavior();
                break;
        }
    }
    protected virtual void OnBehaviorChange(Attacks attacks)
    {
        currentAttackBehaviour = attacks;
    }

    protected virtual void DoGuitarNormalBehavior(){ }
    protected virtual void DoDrumNormalBehavior(){ }
    protected virtual void DoKeyboardNormalBehavior(){ }
    protected virtual void DoVocalNormalBehavior(){ }


}
