using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDoor : MonoBehaviour
{
    [SerializeField] private List<GameObject> mirrorKeys;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LaserProjectile"))
        {
            if (ActivatedAllMirrorKeys())
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    private bool ActivatedAllMirrorKeys()
    {
        foreach (var mirrorKey in mirrorKeys)
        {

            InteractableMirror interactableMirror = mirrorKey.GetComponent<InteractableMirror>();

            if (interactableMirror != null)
            {

                if (!interactableMirror.activatedMirror)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}
