using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public GameObject CurrentlyDisplayedCanvas;
    public GameObject MainMenuCanvas;
    public GameObject SettingCanvas;
    public GameObject SoundCanvas;
    public GameObject ResolutionCanvas;
    public GameObject CreditCanvas;
    public float CurrentVolume;


    void Start()
    {
        //CanvasObject = GetComponent<Canvas>();
        CurrentVolume = 1;

        CurrentlyDisplayedCanvas = MainMenuCanvas;

        CurrentlyDisplayedCanvas.SetActive(true);
    }

    void Update()
    { }

    
    public void OnStartPress()
    {
        CurrentlyDisplayedCanvas.SetActive(false);
        //CurrentlyDisplayedCanvas = MainMenuCanvas;
        //CurrentlyDisplayedCanvas.SetActive(true);
        //CanvasObject.enabled = !CanvasObject.enabled;
        //Debug.Log("GO TO PLATFORMER");
        SceneManager.LoadScene("Platformer");
    }

    public void OnSettingPress()
    {
        CurrentlyDisplayedCanvas.SetActive(false);
        CurrentlyDisplayedCanvas = SettingCanvas;
        CurrentlyDisplayedCanvas.SetActive(true);
        //SceneManager.LoadScene("Settings");
    }

    public void OnSoundPress()
    {
        CurrentlyDisplayedCanvas.SetActive(false);
        CurrentlyDisplayedCanvas = SoundCanvas;
        CurrentlyDisplayedCanvas.SetActive(true);
        //SceneManager.LoadScene("Sound");
    }

    public void OnResolutionPress()
    {
        CurrentlyDisplayedCanvas.SetActive(false);
        CurrentlyDisplayedCanvas = ResolutionCanvas;
        CurrentlyDisplayedCanvas.SetActive(true);
        //SceneManager.LoadScene("Resolution");
    }

    public void OnCreditPress()
    {
        CurrentlyDisplayedCanvas.SetActive(false);
        CurrentlyDisplayedCanvas = CreditCanvas;
        CurrentlyDisplayedCanvas.SetActive(true);
        //SceneManager.LoadScene("Credits");
    }

    public void OnBackPress()
    {
        CurrentlyDisplayedCanvas.SetActive(false);
        CurrentlyDisplayedCanvas = MainMenuCanvas;
        CurrentlyDisplayedCanvas.SetActive(true);
        //SceneManager.LoadScene("MainMenu");
    }

    public void OnOptionsBackPress()
    {
        CurrentlyDisplayedCanvas.SetActive(false);
        CurrentlyDisplayedCanvas = MainMenuCanvas;
        CurrentlyDisplayedCanvas.SetActive(true);
        //SceneManager.LoadScene("Settings");
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
