using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{
    public static Canvas canvas;
    void Start()
    {
        canvas = GameObject.FindWithTag("ResumeCanvas").GetComponent<Canvas>();

        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Canvas with the specified tag not found!");
        }
    }

    public static bool ToggleShow()
    {
        bool isActive = canvas.gameObject.activeSelf;

        if (isActive)
        {
            canvas.gameObject.SetActive(false);
            MainPlayer.LockCursor();
            UnpauseAllAudio();

        }
        else
        {
            canvas.gameObject.SetActive(true);
            MainPlayer.UnlockCursor();
            PauseAllAudio();
        }
        return isActive;
    }

    private static void PauseAllAudio()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Pause();
        }
    }
    private static void UnpauseAllAudio()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Play();
        }
    }

    public void BackToMain()
    {
        SceneLoader.changeScene("LoadingScreen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public static bool ResumeGame()
    {
        return true;
    }


}
