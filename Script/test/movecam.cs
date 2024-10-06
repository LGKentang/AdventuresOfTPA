using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movecam : MonoBehaviour
{
    public Transform cameraPosition;
    public Transform lookAtThisPosition;
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
