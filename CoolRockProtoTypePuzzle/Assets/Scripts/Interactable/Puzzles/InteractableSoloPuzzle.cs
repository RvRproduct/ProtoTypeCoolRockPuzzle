using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSoloPuzzle : Interactable
{
    [SerializeField]
    private int soloLength = 5;
    [SerializeField]
    private float reactDuration = 5;
    private List<SoloPatterns> soloPuzzle;
    private Coroutine soloCoroutine;
    private bool isSolved;
    private SoloPatterns currentPattern;
    private int currentPuzzle;
    private void Start() 
    {
        GenerateRandomSoloPuzzle();
        PlayerInputManager.OnSoloPress += OnSoloPress;
    }

    private void OnSoloPress(SoloPatterns soloPattern)
    {
        if(isSolved) { return;}
        if(soloPattern == currentPattern) 
        { 
            //Debug.Log("SoloSuccess"); 
            currentPuzzle++;
            if(currentPuzzle == soloLength)
            { 
                SoloSuccess(); 
                return;
            }

            //start next solo
            if(soloCoroutine != null) { StopCoroutine(soloCoroutine); }
            soloCoroutine = StartCoroutine(Solo());
        }
        else
        {
            SoloFailed();
        }
    }

    private void GenerateRandomSoloPuzzle()
    {
        soloPuzzle = new List<SoloPatterns>();
        for(int i = 0; i < soloLength; i++)
        {
            int random = UnityEngine.Random.Range(0, 4);
            soloPuzzle.Add((SoloPatterns)random);
        }
    }

    protected override void Interact()
    {
        if(soloCoroutine != null) { return;}
        if(isSolved == true) { return; }
        Debug.Log("Solo Start");
        StartSolo();
    }

    private void StartSolo()
    {
        soloCoroutine = StartCoroutine(Solo());
    }

    private IEnumerator Solo()
    {
        currentPattern = soloPuzzle[currentPuzzle];
        Debug.Log("Press Solo: " + currentPattern);
        yield return new WaitForSeconds(reactDuration);   
        SoloFailed();
        yield return null;
    }

    private void SoloSuccess()
    {
        Debug.Log("Nice Solo");
        isSolved = true;
        StopCoroutine(soloCoroutine);
        soloCoroutine = null;
    }

    private void SoloFailed()
    {
        Debug.Log("Bad Solo :(");
        if(soloCoroutine != null)
        {
            StopCoroutine(soloCoroutine);
            soloCoroutine = null;
        }
        currentPuzzle = 0;
    }

    private void OnDisable() 
    {
        PlayerInputManager.OnSoloPress -= OnSoloPress;
    }


}

public enum SoloPatterns
{
    up = 0,
    down = 1,
    left = 2,
    right = 3,
}
