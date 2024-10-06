using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleCanvas : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI hybridText;

    public GameObject panelInv;
    

    public bool IsActiveInventory;


    public void Start()
    {
        UpdateCounts();
        IsActiveInventory = true;
        panelInv.gameObject.SetActive(true);
    }

    public void UpdateCounts()
    {
        healthText.text = UserAttributes.GetSpecificPotionCount("Health Potion").ToString();
        manaText.text = UserAttributes.GetSpecificPotionCount("Mana Potion").ToString();
        hybridText.text = UserAttributes.GetSpecificPotionCount("Hybrid Potion").ToString();
    }

    public void Update()
    {
        UpdateCounts();

        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryPanel();
        }



    }

    public void ToggleInventoryPanel()
    {
        if (IsActiveInventory)
        {
            panelInv.gameObject.SetActive(true);
            IsActiveInventory = false;
        }
        else
        {
            panelInv.gameObject.SetActive(false);
            IsActiveInventory = true;
        }
    }



}
