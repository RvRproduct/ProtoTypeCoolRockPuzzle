using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNpcD : InteractableNpc
{
    [SerializeField]
    private Transform targetPosition;
    protected override void DoGuitarNormalBehavior()
    {
        
    }
    protected override void DoDrumNormalBehavior()
    {

    }
    protected override void DoKeyboardNormalBehavior()
    {

    }
    protected override void DoVocalNormalBehavior()
    {
        //move to specific position
        var step =  5f * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, step);
    }
}
