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
        if(currentPlayer != null)
        {
            currentPlayer.SetPosition(teleportTarget.position);
            currentAttackBehaviour = Attacks.None; 
            currentPlayer = null;
        }
    }
    protected override void DoKeyboardNormalBehavior()
    {
        
    }
    protected override void DoVocalNormalBehavior()
    {

    }
}
