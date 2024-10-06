using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class UserAttributes
{
    public static float Money { get; set; }
    public static List<Potion> Inventory { get; set; }
    public static int Mana;


    private const int MAX_INV_SIZE = 8;

    static UserAttributes()
    {
        Money = PlayerPrefs.GetFloat("money");
        Inventory = new List<Potion>();
        Mana = 100;
    }

    public static string AddPotion(Potion p)
    {
        if (Inventory.Count < MAX_INV_SIZE)
        {
            Inventory.Add(p);
            return "Success";
        }
        return "Full";
    }

    public static void RemovePotion(string PotionName)
    {
        Potion potion = Inventory.Find(p => p.Name == PotionName);
        if (potion != null)
        {
            Inventory.Remove(potion);
        }
    }

    public static void ReduceMoney(int money)
    {
        Money -= money;
    }

    public static int GetInvSize()
    {
        return Inventory.Count;
    }

   public static bool checkMoney(int price)
   {
        return Money - price < 0 ? false : true;
   }

   public static int GetSpecificPotionCount(string PotionName)
   {
        int total = 0;
        foreach (Potion p in Inventory)
        {
            if (p.Name == PotionName) total++;
        }
        return total;
   }

    public static bool SpecificPotionExist(string PotionName)
    {
        return Inventory.Find(p => p.Name == PotionName) != null;
    }


}
