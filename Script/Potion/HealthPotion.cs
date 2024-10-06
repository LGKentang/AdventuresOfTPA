using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Potion
{
    public HealthPotion()
    {
        Name = "Health Potion";
        Price = 3500;
    }

    public override void UsePotion(Ally ally)
    {
        ally.Health += 40;
        UserAttributes.RemovePotion(Name);
    }
}
