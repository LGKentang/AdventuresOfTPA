using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : Enemy
{
    public PlayerAttacker()
    {
        Priority = "Player";
        Health = 80;
        BasicDamage = .07f;
    
        AttackSpeed = 1.2f;
        MovementSpeed = 1.2f;
    }
}
