using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum MeditationScenes
{
    Sakura,
    Forest,
    Desert,

    Space
}

public class SceneButton : MonoBehaviour
{
    [SerializeField] public MeditationScenes scene;
    public Button button;

    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        SceneManager.LoadScene("Scenes/" + scene.ToString(), LoadSceneMode.Single);
        Debug.Log("You have clicked the button " + scene.ToString() + " !");
    }
}