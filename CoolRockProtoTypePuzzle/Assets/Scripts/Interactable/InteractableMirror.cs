using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMirror : Interactable
{
    [HideInInspector] public bool activatedMirror = false;
    [SerializeField] public float coolDown = 10.0f;
    private float coolDownToUnActivate = 0.0f;

    private void Update()
    {
        if (activatedMirror)
        {
            coolDownToUnActivate += Time.deltaTime;

            if (coolDownToUnActivate >= coolDown)
            {
                activatedMirror = false;
                ResetCoolDown();
            }
        }
    }

    public void ResetCoolDown()
    {
        coolDownToUnActivate = 0.0f;
    }

    protected override void Interact()
    {
        transform.rotation *= Quaternion.Euler(0.0f, 45.0f, 0.0f);
    }
}
