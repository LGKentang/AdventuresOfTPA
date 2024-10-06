using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public string Priority;
    public float Health;
    public float BasicDamage;

    public float AttackSpeed;
    public float MovementSpeed;

    public bool CheckDeath()
    {
        return Health <= 0;
    }

}
