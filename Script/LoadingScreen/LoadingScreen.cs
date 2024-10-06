using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public Canvas settingCanvas;

    void Awake()
    {
       settingCanvas.gameObject.SetActive(false);
    }

    public void PlayGame()
    {
        SceneLoader.changeScene("Main_Scene");
    }

    public void Settings() {
        settingCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
  
