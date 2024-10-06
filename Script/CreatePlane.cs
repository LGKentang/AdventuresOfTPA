using System.Security.Cryptography;
using UnityEngine;

public class CreatePlane : MonoBehaviour
{
    public Transform parent;
    public float planeWidth;
    public float planeLength;


    void Start()
    {
        // Create plane
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = parent.transform.position;
        plane.transform.localScale = new Vector3(planeWidth, 1f, planeLength);
    }
}
