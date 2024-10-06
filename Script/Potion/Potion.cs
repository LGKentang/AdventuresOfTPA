using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Potion
{
    public int Price { get; set; }
    public string Name { get; set; }
    public abstract void UsePotion(Ally ally);
}

