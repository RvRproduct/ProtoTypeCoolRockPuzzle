using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePattern : Interactable
{
    [SerializeField]
    private List<PatternObject> patternObjects; 
    private bool isSolved;
    // Start is called before the first frame update
    void Start()
    {
        if(meshRenderer != null)
        {
            meshRenderer.material.SetColor("_Color", Color.red);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override  void OnTriggerEnter(Collider other) 
    {

    }
    protected override void OnTriggerExit(Collider other)
    {

    }

    public void CheckPuzzle()
    {
        Debug.Log("Start Pattern Puzzle Check");
        isSolved = true;
        for(int i = 0; i < patternObjects.Count; i++)
        {
            if(patternObjects[i].IsActivate == false) { isSolved = false; }
        }
        if(!isSolved) { return; }
        Debug.Log("Pattern Puzzle Solved");
        if(meshRenderer != null)
        {
            meshRenderer.material.SetColor("_Color", Color.green);
        }
    }
}
