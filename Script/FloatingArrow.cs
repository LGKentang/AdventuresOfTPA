using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingArrow : MonoBehaviour
{

    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 playerPosition = playerTransform.position;
        playerPosition.y = transform.position.y;

        Vector3 directionToPlayer = playerPosition - transform.position;
        float angle = Mathf.Atan2(directionToPlayer.x, directionToPlayer.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(180f, angle, 180f);
    }

}
