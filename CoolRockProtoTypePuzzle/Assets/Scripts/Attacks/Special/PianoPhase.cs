using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoPhase : MonoBehaviour
{
    [HideInInspector] public PlayerState playerState;
    [SerializeField] private float positionChange = 15;
    [SerializeField] private float speed = 20;
    private Vector3 targetPosition;
    private Transform parentTransform;
    private CharacterController characterController;

    private void Awake()
    {
        playerState = GetComponentInParent<PlayerController>().PlayerState;
        parentTransform = transform.parent;
        characterController = GetComponentInParent<CharacterController>();
    }

    private void OnEnable()
    {
        playerState.OnPlayerPhaseActivate(!playerState.PlayerPhaseMode);

        if (parentTransform != null && LayerMask.NameToLayer("Phase") != -1)
        {
            parentTransform.gameObject.layer = LayerMask.NameToLayer("Phase");
        }

        targetPosition = parentTransform.position + parentTransform.forward * positionChange;

        MeshRenderer playerMeshRenderer = GetComponentInParent<MeshRenderer>();

        if (playerMeshRenderer != null)
        {
            EnableTransparency(playerMeshRenderer.material);

            Color transparentBlue = Color.blue;
            transparentBlue.a = 0.5f;
            playerMeshRenderer.material.color = transparentBlue;
        }

        float distance = Vector3.Distance(parentTransform.position, targetPosition);
        float lerpDuration = distance / speed;

        StartCoroutine(LerpPosition(targetPosition, lerpDuration));
    }

    private IEnumerator LerpPosition(Vector3 _targetPosition, float lerpTime)
    {
        float timeElapsed = 0f;
        Vector3 startPosition = parentTransform.position;
        Vector3 gravityEffect = Vector3.zero;
        float gravity = -9.81f; // You can modify this value to match your game's gravity
        float verticalVelocity = 0f;

        while (timeElapsed < lerpTime)
        {
            // Apply gravity if not grounded
            if (!characterController.isGrounded)
            {
                verticalVelocity += gravity * Time.deltaTime;
            }
            else
            {
                verticalVelocity = 0f; // Reset vertical velocity when grounded
            }

            Vector3 newPosition = Vector3.Lerp(startPosition, _targetPosition, timeElapsed / lerpTime);
            Vector3 moveDirection = newPosition - parentTransform.position;

            // Add gravity effect to the moveDirection
            moveDirection.y += verticalVelocity * Time.deltaTime;

            // Move the character
            characterController.Move(moveDirection);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Final move to the exact target position
        Vector3 finalMoveDirection = _targetPosition - parentTransform.position;
        finalMoveDirection.y += verticalVelocity * Time.deltaTime;
        characterController.Move(finalMoveDirection);

        OnLerpComplete();
    }

    private void OnLerpComplete()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        playerState.OnPlayerPhaseActivate(!playerState.PlayerPhaseMode);
        GetComponentInParent<Transform>().gameObject.layer = LayerMask.NameToLayer("Player");

        MeshRenderer playerMeshRenderer = GetComponentInParent<MeshRenderer>();

        if (playerMeshRenderer != null)
        {
            DisableTransparency(playerMeshRenderer.material);

            Color opaqueColor = Color.blue;
            opaqueColor.a = 1f;
            playerMeshRenderer.material.color = opaqueColor;
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
}
