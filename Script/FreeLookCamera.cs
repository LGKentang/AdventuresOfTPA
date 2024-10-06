using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreeLookCamera : MonoBehaviour
{
    public CinemachineFreeLook cfl;
    private float x, y;

    void Start()
    {
        x = cfl.m_XAxis.m_MaxSpeed;
        y = cfl.m_YAxis.m_MaxSpeed;
    }

    void Update()
    {
        if (ThirdPersonCam.rotateActive())
        {
            UnlockCamera();
        }
        else
        {
            LockCamera();
        }
    }

    void LockCamera()
    {
        cfl.m_XAxis.m_MaxSpeed = 0;
        cfl.m_YAxis.m_MaxSpeed = 0;
    }

    void UnlockCamera()
    {
        cfl.m_XAxis.m_MaxSpeed = x;
        cfl.m_YAxis.m_MaxSpeed = y;
    }
}
