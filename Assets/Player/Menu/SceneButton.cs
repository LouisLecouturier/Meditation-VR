using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum MeditationScenes
{
    Sakura,
    Forest,
    Desert
}
public class SceneButton : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public MeditationScenes scene;


    void TaskOnClick()
    {
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
    }

}
