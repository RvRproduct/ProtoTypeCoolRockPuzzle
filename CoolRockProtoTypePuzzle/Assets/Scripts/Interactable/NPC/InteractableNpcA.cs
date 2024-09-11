using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNpcA : InteractableNpc
{

    protected override void DoGuitarNormalBehavior()
    {
        //follow player
        var step =  10f * Time.deltaTime;
        if(currentPlayer != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentPlayer.transform.position, step);
        }
    }
    protected override void DoDrumNormalBehavior()
    {
        

    }
    protected override void DoKeyboardNormalBehavior()
    {

    }
    protected override void DoVocalNormalBehavior()
    {

    }
}
