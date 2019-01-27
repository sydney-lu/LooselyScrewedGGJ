using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public Canvas CanvasObject;

    void Start()
    {
        CanvasObject = GetComponent<Canvas>();
    }

    void Update()
    { }

    public void OnStartPress()
    {

        CanvasObject.enabled = !CanvasObject.enabled;
        SceneManager.LoadScene("Platformer");
        Debug.Log("You pressed the play button");
    }

    public void OnSettingPress()
    {
        CanvasObject.enabled = !CanvasObject.enabled;
        SceneManager.LoadScene("Settings");
        Debug.Log("You pressed the settings button");
    }

    public void OnQuitPress()
    {
        Debug.Log("You pressed the quit button");
        Application.Quit();

    }
}
