using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
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

    private void Awake()
    {
        characterController = GetComponentInChildren<CharacterController>();
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        virtualCamera.Follow = transform;
        virtualCamera.LookAt = transform;

        playerState = GetComponent<PlayerController>().PlayerState;
        PlayerInputManager.OnPlayerMovement += onMovementInput;
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
            characterController.Move((currentMovement * playerSpeed) * Time.deltaTime);
        }
        
    }
    private void OnDisable()
    {
        PlayerInputManager.OnPlayerMovement -= onMovementInput;
    }
}
