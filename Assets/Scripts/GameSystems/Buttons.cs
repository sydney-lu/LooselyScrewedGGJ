using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Canvas CanvasObject;
    public float CurrentVolume;


    void Start()
    {
        //CanvasObject = GetComponent<Canvas>();
        CurrentVolume = 1;
    }

    void Update()
    { }

    
    public void OnStartPress()
    {
        CanvasObject.enabled = !CanvasObject.enabled;
        Debug.Log("GO TO PLATFORMER");
        SceneManager.LoadScene("Platformer");
    }

    public void OnSettingPress()
    {
        CanvasObject.enabled = !CanvasObject.enabled;
        SceneManager.LoadScene("Settings");
    }

    public void OnSoundPress()
    {
        CanvasObject.enabled = !CanvasObject.enabled;
        SceneManager.LoadScene("Sound");
    }

    public void OnResolutionPress()
    {
        CanvasObject.enabled = !CanvasObject.enabled;
        SceneManager.LoadScene("Resolution");
    }

    public void OnCreditPress()
    {
        CanvasObject.enabled = !CanvasObject.enabled;
        SceneManager.LoadScene("Credits");
    }

    public void OnBackPress()
    {
        CanvasObject.enabled = !CanvasObject.enabled;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnOptionsBackPress()
    {
        CanvasObject.enabled = !CanvasObject.enabled;
        SceneManager.LoadScene("Settings");
    }

    public void OnSlide(float VolumeValue)
    {
        CurrentVolume = VolumeValue;
    }

    public void OnQuitPress()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
