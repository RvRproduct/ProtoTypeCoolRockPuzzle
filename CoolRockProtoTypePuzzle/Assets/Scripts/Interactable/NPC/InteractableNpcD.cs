using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNpcD : InteractableNpc
{
    [SerializeField]
    private Transform targetPosition;
    private bool startMove = false;
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
        if(currentPlayer == null) { return;}
        startMove = true;
        
    }

    protected override void Update() 
    {
        base.Update();
        Move();
    }

    private void Move()
    {
        if(!startMove) { return;}
        //move to specific position
        var step =  5f * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, step);
    }
}
