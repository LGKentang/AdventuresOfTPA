using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HybridPotion : Potion
{
    public HybridPotion()
    {
        Name = "Hybrid Potion";
        Price = 4000;
    }

    public override void UsePotion(Ally ally)
    {
        ally.Health += 30;
        UserAttributes.Mana += 30;
        UserAttributes.RemovePotion(Name);
    }
}
