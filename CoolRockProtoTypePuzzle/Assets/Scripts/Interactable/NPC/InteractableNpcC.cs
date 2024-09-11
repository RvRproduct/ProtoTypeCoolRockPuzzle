using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNpcC : InteractableNpc
{
    [SerializeField]
    private float speed = 5;
    private float target = 80;
    protected override void Update()
    {
        base.Update();
        var step =  speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, target), step);
        if(transform.position.z < 35)
        {
            target = 80;
        }
        else if(transform.position.z > 75)
        {
            target = 30;
        }
    }
    protected override void DoGuitarNormalBehavior()
    {
        
    }
    protected override void DoDrumNormalBehavior()
    {
        

    }
    protected override void DoKeyboardNormalBehavior()
    {
        if(currentPlayer == null) { return; }
        if(speed > 0 ) { speed = 0; }
        else { speed = 5; }
        currentAttackBehaviour = Attacks.None;
    }
    protected override void DoVocalNormalBehavior()
    {        
        
    }
}
