using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ex_scene : MonoBehaviour
{
    public string sceneName = "ZenMode";

    public void OnClick()
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Scene Loaded");
    }
}

