using System.Net;
using UnityEngine;

public class testcanvas : MonoBehaviour
{
    public static Canvas canvas;

    void Start()
    {
        canvas = GameObject.FindWithTag("QuestCanvas").GetComponent<Canvas>();
    }


    public static void ToggleShow()
    {
        bool isShow = canvas.gameObject.activeSelf;
        UnityEngine.Debug.Log(canvas.gameObject.activeSelf);
        if (isShow)
        {
            canvas.gameObject.SetActive(false);
        }
        else
        {
            canvas.gameObject.SetActive(true);
        }
        UnityEngine.Debug.Log(canvas.gameObject.activeSelf);
    }

}
