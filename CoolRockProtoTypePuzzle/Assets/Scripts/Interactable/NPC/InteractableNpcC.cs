using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNpcC : InteractableNpc
{
    [SerializeField]
    private float speed = 5;
    private float target = -12;
    protected override void Update()
    {
        base.Update();
        var step =  speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target, transform.position.y, transform.position.z), step);
        if(transform.position.x < -10)
        {
            target = 12;
        }
        else if(transform.position.x > 10)
        {
            target = -12;
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
