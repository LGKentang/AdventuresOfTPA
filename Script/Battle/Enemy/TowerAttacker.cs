using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttacker : Enemy
{
    public TowerAttacker()
    {
        Priority = "Tower";
        Health = 250;
        BasicDamage = 3;

        AttackSpeed = .5f;
        MovementSpeed = .5f;
    }

}