using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : Potion
{
    public ManaPotion()
    {
        Name = "Mana Potion";
        Price = 3500;
    }

    public override void UsePotion(Ally ally)
    {
        UserAttributes.Mana += 40;
        UserAttributes.RemovePotion(Name);
    }
}
