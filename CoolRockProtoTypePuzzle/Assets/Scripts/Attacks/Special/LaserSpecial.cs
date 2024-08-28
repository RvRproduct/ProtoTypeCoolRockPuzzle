using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpecial : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform firePoint;
    [HideInInspector] public PlayerState playerState;

    private void Awake()
    {
        playerState = GetComponentInParent<PlayerController>().PlayerState;
    }

    private void OnEnable()
    {
        playerState.OnPlayerShootActivate(!playerState.PlayerShootMode);
        Shoot();
    }

    private void Shoot()
    {
        GameObject laster = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        playerState.OnPlayerShootActivate(!playerState.PlayerShootMode);
    }
}
