using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class MovementController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5f;
    PlayerControl playerControl;
    CharacterController characterController;
    CinemachineVirtualCamera virtualCamera;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    float rotationFactorPerFrame = 15.0f;

    private void Awake()
    {
        playerControl = new PlayerControl();
        characterController = GetComponentInChildren<CharacterController>();
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        virtualCamera.Follow = transform;
        virtualCamera.LookAt = transform;

        playerControl.CharacterControl.Move.started += onMovementInput;
        playerControl.CharacterControl.Move.canceled += onMovementInput;
        playerControl.CharacterControl.Move.performed += onMovementInput;
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

    void onMovementInput (InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
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
        handleGravity();
        handleRotation();
        characterController.Move((currentMovement * playerSpeed) * Time.deltaTime);
    }

    private void OnEnable()
    {
        playerControl.CharacterControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.CharacterControl.Disable();
    }
}
