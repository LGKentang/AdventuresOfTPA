using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public float health = 2000;

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }

    public bool CheckDestroyed()
    {
        return health <= 0;
    }


}
