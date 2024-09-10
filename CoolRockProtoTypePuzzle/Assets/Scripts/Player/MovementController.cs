using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System;
public class MovementController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] private float phasePositionChange = 15;
    [SerializeField] private float phaseSpeed = 20;

    private PlayerState playerState;
    CharacterController characterController;
    CinemachineVirtualCamera virtualCamera;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    float rotationFactorPerFrame = 15.0f;
    bool isAiming;
    Color originColor;

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
        PlayerInputManager.OnPhase += OnPhase;
    }

    private void OnPhase()
    {
        playerState.OnPlayerPhaseActivate(!playerState.PlayerPhaseMode);

        transform.gameObject.layer = LayerMask.NameToLayer("Phase");
        

        Vector3 targetPosition = transform.position + transform.forward * phasePositionChange;

        MeshRenderer playerMeshRenderer = GetComponent<MeshRenderer>();

        originColor = playerMeshRenderer.material.color;
        if (playerMeshRenderer != null)
        {
            EnableTransparency(playerMeshRenderer.material);

            Color transparentColor = Color.cyan;
            transparentColor.a = 0.5f;
            playerMeshRenderer.material.color = transparentColor;
        }

        float distance = Vector3.Distance(transform.position, targetPosition);
        float lerpDuration = distance / phaseSpeed;

        StartCoroutine(LerpPostion(targetPosition, lerpDuration));
    }

    private IEnumerator LerpPostion(Vector3 _targetPosition, float lerpTime)
    {
        float timeElapsed = 0f;
        Vector3 startPosition = transform.position;

        while (timeElapsed < lerpTime)
        {
            Vector3 newPosition = Vector3.Lerp(startPosition, _targetPosition, timeElapsed / lerpTime);
            Vector3 moveDirection = newPosition - transform.position;

            characterController.Move(moveDirection);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        Vector3 finalMoveDirection = _targetPosition - transform.position;
        characterController.Move(finalMoveDirection);

        OnLerpComplete();
    }

    private void OnLerpComplete()
    {
        playerState.OnPlayerPhaseActivate(!playerState.PlayerPhaseMode);
        gameObject.layer = LayerMask.NameToLayer("Player");

        MeshRenderer playerMeshRenderer = GetComponentInParent<MeshRenderer>();

        if (playerMeshRenderer != null)
        {
            DisableTransparency(playerMeshRenderer.material);

            Color opaqueColor = Color.blue;
            opaqueColor.a = 1f;
            playerMeshRenderer.material.color = originColor;
        }
    }

    private void EnableTransparency(Material material)
    {
        material.SetFloat("_Mode", 3);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }

    private void DisableTransparency(Material material)
    {
        material.SetFloat("_Mode", 0);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.DisableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
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
        if (!playerState.PlayerPhaseMode && !playerState.PlayerGrappleMode)
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
