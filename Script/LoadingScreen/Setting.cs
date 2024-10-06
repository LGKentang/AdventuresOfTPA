using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Canvas mainCanvas;
    public AudioMixer audioMixer;

    public Slider volumeSlider;
    public Dropdown resolutionDropdown;

    Resolution[] res;

    private void Start()
    {
        float volume;
        audioMixer.GetFloat("volume",out volume);
        volumeSlider.value = volume;

        res = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> opts = new List<string>();

        int currRes = 0;

        for (int i = 0; i < res.Length; i++)
        {
            string option = res[i].width + " x " + res[i].height;
            opts.Add(option);

            if (res[i].width == Screen.currentResolution.width &&
               res[i].height == Screen.currentResolution.height)
            {
                currRes = i;
            }
        }

        resolutionDropdown.AddOptions(opts);
        resolutionDropdown.value = currRes;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resIdx)
    {
        Resolution reso = res[resIdx];
        Screen.SetResolution(reso.width, reso.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume",volume);
    }

    public void SetQuality(int idx)
    {
        idx = Mathf.Abs(idx - 2);
        QualitySettings.SetQualityLevel(idx);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }


    public void GoBack()
    {
        mainCanvas.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

}
