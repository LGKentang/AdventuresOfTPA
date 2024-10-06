using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("Ref")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj; 
    public Rigidbody rb;


    [Header("Attributes")]
    public float rotationSpeed;

    float horizontalInput;
    float verticalInput;
    Vector3 inputDir;

    static bool allowRotate;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        allowRotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject && player)
        {

            if (allowRotate)
            {
                Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
                orientation.forward = viewDir.normalized;
                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");
                inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

                if (inputDir != Vector3.zero)
                {
                    playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
                }
            }
            else
            {
                horizontalInput = 0f;
                verticalInput = 0f;
                inputDir = Vector3.zero;
            }

        }
    }

    public static void ToggleRotate()
    {
        if (allowRotate)
        {
            allowRotate = false;
        }
        else
        {
            allowRotate = true;
        }
    }

    public static bool rotateActive()
    {
        return allowRotate;
    }

}

