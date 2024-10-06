using System.Net;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopCanvas : MonoBehaviour
{
    public static Canvas canvas;

    [Header("Text")]
    public TextMeshProUGUI money;
    public TextMeshProUGUI inventory;
    public TextMeshProUGUI transaction;
    public TextMeshProUGUI error;
    public TextMeshProUGUI potions;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip coin;

    Potion lastPotion = null;
    int i = 1;

    void Start()
    {
        canvas = GameObject.FindWithTag("ShopCanvas").GetComponent<Canvas>();
        
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Canvas with the specified tag not found!");
        }
    }

    private void Update()
    {
        money.text = Mathf.Floor(UserAttributes.Money).ToString();
        inventory.text = string.Format("{0}/8", UserAttributes.Inventory.Count);

        int hpCount = 0;
        int mpCount = 0;
        int hypCount = 0;

        foreach (Potion potion in UserAttributes.Inventory)
        {
            if (potion.GetType() == typeof(HealthPotion))
            {
                hpCount++;
            }
            else if (potion.GetType() == typeof(ManaPotion))
            {
                mpCount++;
            }
            else if (potion.GetType() == typeof(HybridPotion))
            {
                hypCount++;
            }
        }

        potions.text = string.Format("HP = {0} | MP = {1} | HYP = {2} | Total = {3}",hpCount, mpCount,hypCount, UserAttributes.Inventory.Count);
    }

    public static bool ToggleShow()
    {
        bool isActive = canvas.gameObject.activeSelf;

        if (isActive)
        {
            canvas.gameObject.SetActive(false);
            MainPlayer.LockCursor();

        }
        else
        {
            canvas.gameObject.SetActive(true);
            MainPlayer.UnlockCursor();
        }
        return isActive;
    }


    public void BuyHealthPotion()
    {
        ProcessTransaction(new HealthPotion());
    }

    public void BuyManaPotion()
    {
        ProcessTransaction(new ManaPotion());
    }

    public void BuyHybridPotion()
    {
        ProcessTransaction(new HybridPotion());
    }


    public void ProcessTransaction(Potion p)
    {
        if (UserAttributes.checkMoney(p.Price))
        {
            if (UserAttributes.AddPotion(p) == "Success")
            {
                audioSource.clip = coin;
                audioSource.Play();
                UserAttributes.ReduceMoney(p.Price);
                if (lastPotion != null)
                {
                    if (lastPotion.GetType() == p.GetType()) i++;
                    else
                    {
                        i = 1;
                        lastPotion = p;
                    }
                }
                else
                {
                    i = 1;
                    lastPotion = p;
                }
                ShowTransaction(string.Format("Purchased {0} ({1})",p.Name,i));
            }
            else
            {
                ShowError("Not enough inventory space");
            }
        }
        else
        {
            ShowError("Not enough coins bro");
        }
    }

    public void ShowTransaction(string tr)
    {
        transaction.text = tr;
        error.text = "";
    }

    public void ShowError(string err)
    {
        error.text = err;
        transaction.text = "";
    }

   




}
