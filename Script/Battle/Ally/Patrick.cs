using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrick : Ally
{
    public Patrick()
    {
        IsActive = false;
        Health = 600;

        AttackSpeed = .3f;
        MovementSpeed = .9f;

        PrimaryDamage = 25;
        SecondaryDamage = 35;
        UltimateDamage = 45;
    }
}
