using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = new Vector3(player.transform.position.x, mainCamera.transform.position.y, player.transform.position.z - 13);
    }
}
