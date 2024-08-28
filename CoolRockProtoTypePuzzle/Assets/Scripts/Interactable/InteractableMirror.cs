using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMirror : Interactable
{
    protected override void Interact()
    {
        transform.rotation *= Quaternion.Euler(0.0f, 45.0f, 0.0f);
    }
}
