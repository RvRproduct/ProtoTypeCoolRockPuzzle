using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSimon : Interactable
{
    [SerializeField]
    private int puzzleLength = 5;
    [SerializeField]
    private float reactDuration = 5;
    [SerializeField]
    private float showColorDuration = 1;
    private List<PlayerInstrumentType> simonPuzzle;
    private int currentPuzzle;
    private int currentStage;
    private PlayerInstrumentType currentType;
    private bool isSolved;
    private Coroutine colorCoroutine;
    private Coroutine reactCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomSimonPuzzle();
        PlayerInputManager.OnNormalAttack += CheckPuzzle;
        currentType = simonPuzzle[currentPuzzle];
    }

    protected override void Interact()
    {
        ShowPuzzle();
    }

    private void CheckPuzzle()
    {
        if(isSolved) { return;}
        if(player == null) { return;}
        if(colorCoroutine != null) { return;}
        if(currentType == player.PlayerState.CurrnetInstrument)
        {
            Debug.Log("Correct: " + currentPuzzle);
            currentPuzzle++;
            if(currentPuzzle > currentStage) 
            { 
                currentStage++; 
                currentPuzzle = 0;
                if(reactCoroutine != null) { StopCoroutine(reactCoroutine); }
                if(currentStage >= simonPuzzle.Count) 
                { 
                    PuzzleSuccess(); 
                    return;
                }
                ShowPuzzle();
            }
            currentType = simonPuzzle[currentPuzzle];
        }
        else
        {
            PuzzleFailed();
        }
    }


    private void ShowPuzzle()
    {
        if(colorCoroutine != null) { return;}
        if(isSolved) { return; }
        Debug.Log("Start Simon Says");
        colorCoroutine = StartCoroutine(ShowColor());
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        //base.OnTriggerEnter(other);
    }
    protected override void OnTriggerExit(Collider other)
    {
        //base.OnTriggerExit(other);
    }

    private IEnumerator ShowColor()
    {
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < currentStage + 1; i++)
        {
            switch(simonPuzzle[i])
            {
                case PlayerInstrumentType.Guitar: 
                    SetColor(Color.red);  
                    break;
                case PlayerInstrumentType.Drum:
                    SetColor(Color.green);  
                    break;
                case PlayerInstrumentType.Keyboard:
                    SetColor(Color.blue);  
                    break;
                case PlayerInstrumentType.Vocal:
                    SetColor(Color.gray);  
                    break;
            }
            yield return new WaitForSeconds(showColorDuration);
            SetColor(Color.white);
            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
        colorCoroutine = null;
        reactCoroutine = StartCoroutine(ReactCountDown());
    }

    private IEnumerator ReactCountDown()
    {
        yield return new WaitForSeconds(reactDuration);
        PuzzleFailed();
    }

    private void GenerateRandomSimonPuzzle()
    {
        simonPuzzle = new List<PlayerInstrumentType>();
        for(int i = 0; i < puzzleLength; i++)
        {
            int random = UnityEngine.Random.Range(0, 4);
            simonPuzzle.Add((PlayerInstrumentType)random);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PuzzleFailed()
    {
        Debug.Log("SIMON FAILED");
        currentPuzzle = 0;
        currentStage = 0;
        currentType = simonPuzzle[0];
        if(colorCoroutine != null) { StopCoroutine(colorCoroutine); }
        if(reactCoroutine != null) { StopCoroutine(reactCoroutine); }
    }
    private void PuzzleSuccess()
    {
        Debug.Log("SIMON SUCCESS");
        isSolved = true;
        SetColor(Color.black);
    }
}
