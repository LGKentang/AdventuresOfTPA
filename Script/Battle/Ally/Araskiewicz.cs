using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Araskiewicz : Ally
{
    public Araskiewicz()
    {
        IsActive = false;
        Health = 600;

        AttackSpeed = .5f;
        MovementSpeed = .9f;

        PrimaryDamage = 20;
        SecondaryDamage = 0;
        UltimateDamage = 40;
    }
}
