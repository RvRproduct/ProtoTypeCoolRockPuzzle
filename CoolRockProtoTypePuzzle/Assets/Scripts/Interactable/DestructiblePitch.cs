using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DestructiblePitch : MonoBehaviour
{
    private bool pitchFinished = false;
    private int currentPitchLevel = 0;
    private int startPitchLevel = 0;
    private int endPitchLevel = 2;

    [SerializeField] Vector2 pitchLevel1 = Vector2.one;
    [SerializeField] Vector2 pitchLevel2 = Vector2.one;
    [SerializeField] Vector2 pitchLevel3 = Vector2.one;
    public Vector2 PitchLevel1
    {
        get { return pitchLevel1; }
        set
        {
            pitchLevel1.x = Mathf.Clamp(value.x, 0f, 1f);
            pitchLevel1.y = Mathf.Clamp(value.y, 0f, 1f);
        }
    }

    public Vector2 PitchLevel2
    {
        get { return pitchLevel2; }
        set
        {
            pitchLevel2.x = Mathf.Clamp(value.x, 0f, 1f);
            pitchLevel2.y = Mathf.Clamp(value.y, 0f, 1f);
        }
    }

    public Vector2 PitchLevel3
    {
        get { return pitchLevel3; }
        set
        {
            pitchLevel3.x = Mathf.Clamp(value.x, 0f, 1f);
            pitchLevel3.y = Mathf.Clamp(value.y, 0f, 1f);
        }
    }


    private void OnValidate()
    {
        PitchLevel1 = pitchLevel1;
        PitchLevel2 = pitchLevel2;
        PitchLevel3 = pitchLevel3;
    }

    [SerializeField] private float mustHitPitchTime = 0.5f;
    private float currentHitPitchTime = 0f;

    private bool hittingPitchThreshHold = false;

    [SerializeField] private float resetPitchFailed = 0.5f;
    private float currentMissHitPitchTime = 0f;

    private void Update()
    {
        if (pitchFinished)
        {
            Destroy(gameObject);
        }

        if (currentPitchLevel >= startPitchLevel && currentPitchLevel <= endPitchLevel)
        {
            if (hittingPitchThreshHold)
            {
                currentHitPitchTime += Time.deltaTime;


            }
            else
            {
                currentMissHitPitchTime += Time.deltaTime;
            }
        }

        pitchSucceeded();
        pitchFailed();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Collider>().gameObject.CompareTag("Pitch"))
        {

        }
    }

    private void pitchSucceeded()
    {
        if (currentHitPitchTime >= mustHitPitchTime)
        {
            if (currentPitchLevel < endPitchLevel)
            {
                currentPitchLevel++;
                currentHitPitchTime = 0;
                currentMissHitPitchTime = 0;
            }
            else
            {
                pitchFinished = true;
            }
        }
    }

    private void pitchFailed()
    {
        if (currentMissHitPitchTime >= resetPitchFailed)
        {
            if (currentPitchLevel > startPitchLevel && currentPitchLevel <= endPitchLevel)
            {
                currentPitchLevel--;
            }
        }
    }



}
