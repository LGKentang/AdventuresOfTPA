using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{

    public void Start()
    {
        if (!PlayerPrefs.HasKey("money"))
        {
            PlayerPrefs.SetFloat("money", 20000);
        }
    }
    public void StartBattle()
    {
        SceneLoader.changeScene("Battle");
    }

    public void test()
    {
        Debug.Log("test");
    }

}
