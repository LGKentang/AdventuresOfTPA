using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Battle : MonoBehaviour
{
    public List<PlayerController> playerControllers;
    public List<Transform> playerTransform;

    public List<BattleThirdCam> btcs;
    public Crystal crystal;
    private int activePlayerIndex = 0;
    int started = 0;
    public static int score;
    public static int KillCount;

    public float slowMotionFactor = .1f;
    public CanvasGroup whiteout;

    public TextMeshProUGUI time;
    public List<GameObject> allEnemy;

    public Camera wizcam;

    float runningTime;

    public List<HealthBar> hbs;
    public HealthBar mb;
    public HealthBar cyb;

    private void Start()
    {
        score = 0;
        runningTime = 0;

        foreach (HealthBar hb in hbs)
        {
            hb.gameObject.SetActive(false);
        }

        foreach (PlayerController pc in playerControllers)
        {
            pc.SetActiveCamera(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mb.SetMaxHealth(UserAttributes.Mana);
        cyb.SetMaxHealth(crystal.health);
        whiteout.alpha = 0;
    }

    bool AreAllEnemiesDestroyed()
    {
        foreach (GameObject enemy in allEnemy)
        {
            if (enemy != null && enemy.activeSelf)
            {
                return false;
            }
        }

        return true;
    }

    public void ScoreCalculation()
    {
        PlayerPrefs.SetFloat("score", runningTime * 100 + KillCount * 500);
    }

    private void Update()
    {
        if (started == 0)
        {
            playerControllers[0].SetActiveCamera(true);
            playerControllers[0].IsBeingControlled = true;
            btcs[0].isBeingControlled = true;
            hbs[0].gameObject.SetActive(true);
            started++;
        }


        UpdateManaAndCrystal();
        PlayerInput();
        CheckGameOver();

        runningTime += Time.deltaTime;
        TimeUpdater(runningTime);

    }

    void TimeUpdater(float timeDisplay)
    {
        timeDisplay += 1;
        float m = Mathf.FloorToInt(timeDisplay / 60);
        float s = Mathf.FloorToInt(timeDisplay % 60);
        time.text = string.Format("{0:00} : {1:00}", m, s);

    }

    void UpdateManaAndCrystal()
    {
       mb.SetHealth(UserAttributes.Mana);
       cyb.SetHealth(crystal.health);
    }

    void SlowDownGame()
    {
        Time.timeScale = slowMotionFactor;
        Time.fixedDeltaTime = .02f * Time.timeScale;
        while (whiteout.alpha < 1)
        {
            whiteout.alpha += Time.fixedDeltaTime;
        }
    }

    bool AreAllAlliesDied()
    {
        foreach(PlayerController pc in playerControllers)
        {
            if (!pc.getDeath())
            {
                return false;
            }
        }
        return true;
    }

    void CheckGameOver()
    {
        if (crystal.CheckDestroyed() || AreAllEnemiesDestroyed() || AreAllAlliesDied())
        {
            float sec = .15f;
            SlowDownGame();

            StartCoroutine(GameOverAfter(sec));
        }
    }
    IEnumerator GameOverAfter(float sec)
    {
        yield return new WaitForSeconds(sec);
        GameOver();
    }

    void GameOver() {
        ScoreCalculation();
        SceneLoader.changeScene("GameOverScene");
    }

    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (activePlayerIndex == 2)
            {
                if (wizcam.gameObject.activeSelf)
                {
                    //wizcam.gameObject.SetActive(false);
                    playerControllers[activePlayerIndex].SwitchToTPS();
                }
            }

            playerControllers[activePlayerIndex].IsBeingControlled = false;
            playerControllers[activePlayerIndex].SetActiveCamera(false);
            btcs[activePlayerIndex].isBeingControlled = false;
            hbs[activePlayerIndex].gameObject.SetActive(false);

            activePlayerIndex++;
            if (activePlayerIndex >= playerControllers.Count)
            {
                activePlayerIndex = 0;
            }

            playerControllers[activePlayerIndex].IsBeingControlled = true;
            playerControllers[activePlayerIndex].SetActiveCamera(true);
            btcs[activePlayerIndex].isBeingControlled = true;
            hbs[activePlayerIndex].gameObject.SetActive(true);
        }
    }
}
