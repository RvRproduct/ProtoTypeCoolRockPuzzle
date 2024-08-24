using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCube : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Cube say hi!");
    }
}
