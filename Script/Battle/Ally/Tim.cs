using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tim : Ally
{
    public Tim()
    {
        IsActive = false;
        Health = 350;

        AttackSpeed = 0.5f;
        MovementSpeed = 1;

        PrimaryDamage = 15;
        SecondaryDamage = 25;
        UltimateDamage = 40;
    }

}
