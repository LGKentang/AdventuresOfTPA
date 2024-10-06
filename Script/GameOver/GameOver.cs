using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI score;
    private float normalizedTime = 1f;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        float player_score = PlayerPrefs.GetFloat("score");
        score.text = Mathf.Floor(player_score).ToString();
        PlayerPrefs.SetFloat("money",PlayerPrefs.GetFloat("money")+player_score);
        RestartData();
        SaveData();
        NormalizeSpeed();
    }

    public void RestartData()
    {
        PlayerPrefs.SetFloat("score", 0);
    }

    public void SaveData()
    {
        PlayerPrefs.Save();
    }

    public void NormalizeSpeed()
    {
        Time.timeScale = normalizedTime;
        Time.fixedDeltaTime = .02f * Time.timeScale;
    }

    public void RetryGame()
    {
        SceneLoader.changeScene("Battle");
    }

    public void BackToMenu()
    {
        SceneLoader.changeScene("Main_Scene");
    }


}
