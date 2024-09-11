using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiblePitch : MonoBehaviour
{
    private int currentPitchLevel = 0;
    private int startPitchLevel = 0;
    private int endPitchLevel = 2;
    [SerializeField] private int squareGrid = 1;

    [SerializeField] private bool grow = false;

    [SerializeField] List<Vector2> pitchLevels = new List<Vector2> { new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1) };

    private void Start()
    {
        if (grow)
        {
            transform.parent.localScale = new Vector3(4, 4, 4);
        }
        else
        {
            transform.parent.localScale = new Vector3(8, 8, 8);
        }
    }

    public List<Vector2> PitchLevels
    {
        get { return pitchLevels; }
        set
        {
            for (int i = 0; i < pitchLevels.Count; i++)
            {
                pitchLevels[i] = new Vector2(
                    Mathf.Clamp(Mathf.RoundToInt(value[i].x), 0, squareGrid),
                    Mathf.Clamp(Mathf.RoundToInt(value[i].y), 0, squareGrid)
                );
            }
        }
    }

    private void OnValidate()
    {
        PitchLevels = pitchLevels;
    }

    [SerializeField] private float mustHitPitchTime = 1.0f;
    private float currentHitPitchTime = 0f;

    private bool hittingPitchThreshHold = false;
    private Vector2 hitPitchThreshHold;
    private bool setHitPitchThreshHold = false;

    [SerializeField] private float resetPitchFailed = 3.0f;
    private float currentMissHitPitchTime = 0f;

    private bool closeEnough = false;

    private void Update()
    {
        if (closeEnough)
        {
            if (currentPitchLevel >= startPitchLevel && currentPitchLevel <= endPitchLevel)
            {
                if (hittingPitchThreshHold)
                {
                    currentHitPitchTime += Time.deltaTime;
                    visualIfInPitchRange();
                }
                else
                {
                    currentMissHitPitchTime += Time.deltaTime;
                }
            }

            pitchSucceeded();
            // pitchFailed();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Pitch"))
        {
            closeEnough = true;
            setPitchThreshHold();
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Pitch"))
        {
            if (setHitPitchThreshHold)
            {
                pitchThreshHold(collider.gameObject.GetComponent<VocalPitching>().playerState);
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Pitch"))
        {
            closeEnough = false;
            setHitPitchThreshHold = false;
            hittingPitchThreshHold = false;
        }
    }

    private void pitchThreshHold(PlayerState playerState)
    {

        if (fixPitchThreshHold(playerState.PlayerPitch) == hitPitchThreshHold)
        {
            hittingPitchThreshHold = true;
        }
        else
        {
            hittingPitchThreshHold = false;
        }
    }

    private Vector2 fixPitchThreshHold(Vector2 playerPitch)
    {
        Vector2 correctPitchValue = playerPitch;

        if (correctPitchValue.x < 0)
        {
            correctPitchValue.x += 0.0001f;
        }
        else if (correctPitchValue.x > 0)
        {
            correctPitchValue.x -= 0.0001f;
        }

        if (correctPitchValue.y < 0)
        {
            correctPitchValue.y += 0.0001f;
        }
        else if (correctPitchValue.y > 0)
        {
            correctPitchValue.y -= 0.0001f;
        }

        correctPitchValue += new Vector2(1, 1);
        correctPitchValue *= (squareGrid / 2f);

        correctPitchValue.x = Mathf.Clamp(Mathf.RoundToInt(correctPitchValue.x), 0, squareGrid);
        correctPitchValue.y = Mathf.Clamp(Mathf.RoundToInt(correctPitchValue.y), 0, squareGrid);

        return correctPitchValue;
    }

    private void setPitchThreshHold()
    {
        hitPitchThreshHold = pitchLevels[currentPitchLevel];
        setHitPitchThreshHold = true;
    }

    private void visualIfInPitchRange()
    {
        transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, transform.parent.rotation * Quaternion.Euler(0, 5, 0), Time.deltaTime * 8.0f);
    }

    private void onPitchChangeSize()
    {
        if (grow)
        {
            transform.parent.localScale += new Vector3(2, 2, 2);
        }
        else
        {
            transform.parent.localScale -= new Vector3(2, 2, 2);
        }
    }


    private void onPitchLevelComplete()
    {
        transform.parent.transform.localScale -= new Vector3(2, 2, 2);
    }

    private void onPitchLevelIncomplete()
    {
        transform.parent.transform.localScale += new Vector3(2, 2, 2);
    }

    private void pitchSucceeded()
    {
        if (currentHitPitchTime >= mustHitPitchTime)
        {
            if (currentPitchLevel < endPitchLevel)
            {
                currentPitchLevel++;
                onPitchChangeSize();
                setHitPitchThreshHold = false;
                setPitchThreshHold();
                currentHitPitchTime = 0;
                currentMissHitPitchTime = 0;

            }
            else
            {
                grow = !grow;
                pitchLevels.Reverse();
                currentPitchLevel = 0;
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
                onPitchLevelIncomplete();
                setHitPitchThreshHold = false;
                setPitchThreshHold();
                currentHitPitchTime = 0;
                currentMissHitPitchTime = 0;
            }
        }
    }
}
