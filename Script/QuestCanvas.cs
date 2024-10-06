using System.Net;
using UnityEngine;

public class QuestCanvas : MonoBehaviour
{
    public static Canvas canvas;

    void Start()
    {
        canvas = GameObject.FindWithTag("QuestCanvas").GetComponent<Canvas>();

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

        }
        else
        {
            canvas.gameObject.SetActive(true);
            MainPlayer.UnlockCursor();
        }
        return isActive;
    }



}
