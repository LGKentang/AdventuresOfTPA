using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
