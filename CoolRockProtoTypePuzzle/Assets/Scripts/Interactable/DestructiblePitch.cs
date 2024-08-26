using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiblePitch : MonoBehaviour
{
    private bool pitchFinished = false;
    private int currentPitchLevel = 0;
    private int startPitchLevel = 0;
    private int endPitchLevel = 2;

    [SerializeField] List<Vector2> pitchLevels = new List<Vector2> { new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1) };
    [SerializeField] float hitPitchTheshHoldRange = 0.20f;
    public List<Vector2> PitchLevels
    {
        get { return pitchLevels; }
        set
        {
            for (int i = 0; i <pitchLevels.Count; i++)
            {
                pitchLevels[i] = new Vector2(
                    Mathf.Clamp(value[i].x, -1f, 1f),
                    Mathf.Clamp(value[i].y, -1f, 1f)
                );
            }
        }
    }

    public float PitchThreshHoldRange
    {
        get { return hitPitchTheshHoldRange; }
        set
        {
           hitPitchTheshHoldRange = Mathf.Clamp(value, 0f, 1f);
        }
    }

    private void OnValidate()
    {
        PitchThreshHoldRange = hitPitchTheshHoldRange;
        PitchLevels = pitchLevels;
    }

    [SerializeField] private float mustHitPitchTime = 1.0f;
    private float currentHitPitchTime = 0f;

    private bool hittingPitchThreshHold = false;
    private Vector2 hitPitchThreshHoldMax;
    private Vector2 hitPitchThreshHoldMin;
    private bool setHitPitchThreshHold = false;

    [SerializeField] private float resetPitchFailed = 2.0f;
    private float currentMissHitPitchTime = 0f;

    private bool closeEnough = false;

    private void Update()
    {
        if (pitchFinished)
        {
            Destroy(gameObject);
        }

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
            pitchFailed();
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
                Debug.Log(collider.gameObject.GetComponent<VocalPitching>().playerState.PlayerPitch);
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
        if (playerState.PlayerPitch.x >= hitPitchThreshHoldMin.x &&
            playerState.PlayerPitch.y >= hitPitchThreshHoldMin.y &&
            playerState.PlayerPitch.x <= hitPitchThreshHoldMax.x &&
            playerState.PlayerPitch.y <= hitPitchThreshHoldMax.y )
        {
            hittingPitchThreshHold = true;
        }
        else
        {
            hittingPitchThreshHold = false;
        }
    }

    private void setPitchThreshHold()
    {
        hitPitchThreshHoldMax.x = Mathf.Clamp(pitchLevels[currentPitchLevel].x + hitPitchTheshHoldRange, -1f, 1f);
        hitPitchThreshHoldMax.y = Mathf.Clamp(pitchLevels[currentPitchLevel].y + hitPitchTheshHoldRange, -1f, 1f);
        hitPitchThreshHoldMin.x = Mathf.Clamp(pitchLevels[currentPitchLevel].x - hitPitchTheshHoldRange, -1f, 1f);
        hitPitchThreshHoldMin.y = Mathf.Clamp(pitchLevels[currentPitchLevel].y - hitPitchTheshHoldRange, -1f, 1f);

        setHitPitchThreshHold = true;
    }

    private void visualIfInPitchRange()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(0, 5, 0), Time.deltaTime * 8.0f);
    }

    private void onPitchLevelComplete()
    {
        transform.localScale -= new Vector3(2, 2, 2);
    }

    private void onPitchLevelIncomplete()
    {
        transform.localScale += new Vector3(2, 2, 2);
    }

    private void pitchSucceeded()
    {
        if (currentHitPitchTime >= mustHitPitchTime)
        {
            if (currentPitchLevel < endPitchLevel)
            {
                currentPitchLevel++;
                onPitchLevelComplete();
                setHitPitchThreshHold = false;
                setPitchThreshHold();
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
                onPitchLevelIncomplete();
                setHitPitchThreshHold = false;
                setPitchThreshHold();
                currentHitPitchTime = 0;
                currentMissHitPitchTime = 0;
            }
        }
    }
}
