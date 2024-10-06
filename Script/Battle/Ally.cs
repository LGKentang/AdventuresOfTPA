using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally
{
    //public FreeLookCamera PlayerCam;

    public bool IsActive;
    public float Health;
    public float AttackSpeed;
    public float MovementSpeed;

    public int PrimaryDamage;
    public int SecondaryDamage;
    public int UltimateDamage;

    public void TakeDamage(float dmg) {
        this.Health -= dmg;
    }

    public bool CheckDeath()
    {
        return Health <= 0;
    }


}
