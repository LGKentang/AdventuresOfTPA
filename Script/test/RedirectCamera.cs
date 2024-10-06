using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectCamera : MonoBehaviour
{
    Quaternion q;

    public Quaternion getQuater() {
        return q;
    }

    private void Update()
    {
        q = Quaternion.LookRotation(transform.forward);
    }

    
}
