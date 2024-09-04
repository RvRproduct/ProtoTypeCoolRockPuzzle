using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System;
public class MovementController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5f;
    private PlayerState playerState;
    CharacterController characterController;
    CinemachineVirtualCamera virtualCamera;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    float rotationFactorPerFrame = 15.0f;
    bool isAiming;

    private void Awake()
    {
        characterController = GetComponentInChildren<CharacterController>();
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        virtualCamera.Follow = transform;
        virtualCamera.LookAt = transform;

        playerState = GetComponent<PlayerController>().PlayerState;
    }

    private void Start()
    {
        PlayerInputManager.OnPlayerMovement += onMovementInput;
        PlayerInputManager.OnAiming += OnAiming;
    }

    private void OnAiming(bool value)
    {
        isAiming = value;
        Debug.Log("IsAiming: " + isAiming);
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
        
    }

    void onMovementInput (Vector2 movement)
    {
        currentMovementInput = movement;
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundedGravity = -0.05f;
            currentMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity;
        }
    }

    void Update()
    {
        if (!playerState.PlayerPhaseMode)
        {
            handleGravity();
            handleRotation();
            if(!isAiming)
            {
                characterController.Move((currentMovement * playerSpeed) * Time.deltaTime);
            }
        }
        
    }
    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if(isAiming)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 50, Color.red);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 50, layerMask))
            {
                Debug.Log("Hit something");
            }
        }
    }
    private void OnDisable()
    {
        PlayerInputManager.OnPlayerMovement -= onMovementInput;
    }
}
