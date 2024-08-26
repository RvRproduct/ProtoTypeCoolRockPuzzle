using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNpcB : InteractableNpc
{
    [SerializeField]
    private Transform teleportTarget;
    protected override void DoGuitarNormalBehavior()
    {
        
    }
    protected override void DoDrumNormalBehavior()
    {
        //teleport
        currentPlayer.SetPosition(teleportTarget.position);
        currentAttackBehaviour = Attacks.None; 
    }
    protected override void DoKeyboardNormalBehavior()
    {
        
    }
    protected override void DoVocalNormalBehavior()
    {

    }
}
