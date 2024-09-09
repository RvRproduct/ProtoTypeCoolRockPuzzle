using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DrumSpecial : MonoBehaviour
{
    private PlayerState playerState;
    [SerializeField]
    private GameObject drumAttackPrefab;
    [SerializeField]
    private float waveSize = 3;
    [SerializeField]
    private float speed = 10;
    private Coroutine waveGrowCoroutine;
    private GameObject currentWave;
    // Start is called before the first frame update
    private void Awake()
    {
        playerState = GetComponentInParent<PlayerController>().PlayerState;
    }
    
    private void OnEnable()
    {
        playerState.OnPlayerDrumAttackActivate(!playerState.PlayerShootMode);
        DrumAttack();
    }

    private void DrumAttack()
    {
        Debug.Log("Drum Attack");
        if(waveGrowCoroutine != null)
        {
            if(currentWave) { Destroy(currentWave); }
            StopCoroutine(waveGrowCoroutine);
            waveGrowCoroutine = null;
        }
        currentWave = Instantiate(drumAttackPrefab, this.transform);
        waveGrowCoroutine = StartCoroutine(WaveGrow(currentWave));
    }

    private IEnumerator WaveGrow(GameObject wave)
    {
        for(int i = 0; i < waveSize * 10 ; i++)
        {
            wave.transform.localScale += new Vector3(0.1f, 0, 0.1f);
            yield return new WaitForSeconds(1 / speed);
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(wave);
        currentWave = null;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
