using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Actor : MonoBehaviour
{
    #region Audio
    protected AudioSource m_audioSource;
    public AudioSource SFXSource
    {
        get { return m_audioSource; }
        set { m_audioSource = value; }
    }

    public void PlayAudio(AudioClip clip)
    {
        GameManager.AudioManager.playAudio(m_audioSource, clip);
    }
    #endregion

    #region Main
    protected virtual void Awake()
    {
        m_audioSource = GetComponentInChildren<AudioSource>();
    }

    protected virtual void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneUnLoaded;
    }
    protected virtual void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded -= OnSceneUnLoaded;
    }

    protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode) { }
    protected virtual void OnSceneUnLoaded(Scene scene, LoadSceneMode mode) { }
    #endregion
}
