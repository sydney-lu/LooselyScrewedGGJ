using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager Instance;
    public static AudioDictonary AudioManager;
    public static PlayerController player;

    private string m_mainMenuScene = "MainMenu";
    private string m_playScene = "Platformer";
    
    public Canvas m_hudCanvas;
    public Canvas m_pauseCanvas;

    private static bool m_gameOver = false;
    public bool GameOver
    {
        get { return m_gameOver; }
        set { m_gameOver = value; }
    }
    private static bool m_paused = false;
    public bool IsPaused
    {
        get { return m_paused; }
        set { m_paused = value; }
    }
    #endregion

    #region Pause Functions
    public void OnApplicationFocus(bool focus)
    {
        if (this != Instance)
            return;

        if (SceneManager.GetActiveScene().name == m_playScene)
        {
            if (!focus && !m_paused && !Application.isEditor)
                TogglePause(true);
        }
    }

    public void TogglePause()
    {
        if (this != Instance)
            return;

        m_paused = !m_paused;
        Time.timeScale = m_paused ? 0 : 1;
        ToggleCursor(m_paused);
        m_pauseCanvas.gameObject.SetActive(m_paused);
    }

    public void TogglePause(bool pause)
    {
        if (this != Instance)
            return;

        m_paused = pause;
        Time.timeScale = pause ? 0 : 1;
        ToggleCursor(pause);
        m_pauseCanvas.gameObject.SetActive(pause);
    }

    public void ToggleCursor(bool value)
    {
        if (value)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ToggleStateMachinesPause()
    {
        StateManager[] managers = FindObjectsOfType<StateManager>();
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].IsPaused = m_paused;
        }
    }
    #endregion

    #region Player Managment
    public GameObject playerPrefab;
    public static LevelManager levelSpawn;

    //private void SpawnPlayer()
    //{
    //    Transform spawnTransfrom = levelSpawn ? levelSpawn.GetNextSpawn() : transform;
    //    Instantiate(playerPrefab, spawnTransfrom.position, spawnTransfrom.rotation).GetComponent<Actor>();
    //}

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion

    #region Scene Managment
    //public void MainMenu()
    //{
    //    SceneManager.LoadScene(m_mainMenuScene);
    //}

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //    SceneManager.sceneUnloaded += OnSceneUnloaded;
    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //    SceneManager.sceneUnloaded -= OnSceneUnloaded;
    //}

    //public void Continue()
    //{
    //    int sceneIndex = PlayerPrefs.GetInt("ContinueScene");
    //    LoadScene(sceneIndex);
    //}

    //public void LoadScene(string name)
    //{
    //    SceneManager.LoadScene(name);
    //    m_hudCanvas.enabled = !m_hudCanvas.enabled;
    //}
    //public void LoadScene(int index)
    //{
    //    SceneManager.LoadScene(index);
    //}

    //public void AddScene(string name)
    //{
    //    Debug.Log("GameManager:AddSceneByName (" + name + ")", this);
    //    SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
    //}
    //public void AddScene(int index)
    //{
    //    Debug.Log("GameManager:AddSceneByIndex (" + index + ")", this);
    //    SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
    //}

    //public void UnloadScene(string name)
    //{
    //    Debug.Log("GameManager:UnloadSceneByName (" + name + ")", this);
    //    if (SceneManager.GetSceneByName(name).isLoaded)
    //        SceneManager.UnloadSceneAsync(name);
    //}
    //public void UnloadScene(int index)
    //{
    //    Debug.Log("GameManager:UnloadSceneByIndex (" + index + ")", this);
    //    if (SceneManager.GetSceneAt(index).isLoaded)
    //        SceneManager.UnloadSceneAsync(index);
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if (this != Instance)
    //        return;

    //    // Main Menu Scene
    //    else if (scene.name == m_mainMenuScene)
    //    {
    //        m_paused = false;
    //        ToggleCursor(true);
    //        Time.timeScale = 1;
    //        if (player) Destroy(player.gameObject);
    //    }

    //    // Base Play Scene 
    //    else if (scene.name == m_playScene)
    //    {
    //        //if (!player) SpawnPlayer();
    //        ToggleCursor(false);
    //    }

    //    // LevelScene Added To PlayScene
    //    else
    //    {
    //        string currentLevel = scene.name;
    //        PlayerPrefs.SetString("ContinueScene", currentLevel);
    //        SceneManager.SetActiveScene(SceneManager.GetSceneByName("PlayScene"));
    //    }
    //}

    //private void OnSceneUnloaded(Scene scene)
    //{
    //}
    #endregion

    #region Main
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            StopAllCoroutines();
            DestroyImmediate(gameObject);
        }
    }

    private void Update()
    {
        if (this != Instance)
            return;

        if (Input.GetButtonDown("Pause") && SceneManager.GetActiveScene().name == m_playScene)
        {
            if (m_paused) TogglePause(false);
            else TogglePause(true);
        }

        // For Testing
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
                Time.timeScale++;
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
                Time.timeScale--;
            Mathf.Clamp(Time.timeScale, 0, 10);
        }
    }
    #endregion
}
