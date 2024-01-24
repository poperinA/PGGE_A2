using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE.Patterns;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Variables

    //renamed variable for readability
    private bool isPaused;

    #endregion

    #region Unity Callbacks

    void Start()
    {
        isPaused = false;
        LoadInitialScene("Menu");
    }

    //method extraction - CheckPauseInput, TogglePause, PauseGame, and ResumeGame to make code modular
    void Update()
    {
        CheckPauseInput();
    }

    #endregion

    #region Pause Mechanism

    //to check if escape key is pressed
    private void CheckPauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseState();
        }
    }

    //to toggle between pausing/resuming
    private void TogglePauseState()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    //pauses the game
    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    //resumes the game
    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    #endregion

    #region Scene Events

    private void OnEnable()
    {
        SceneManager.sceneLoaded += LogSceneDetails;
        SceneManager.sceneLoaded += WelcomeToScene;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= WelcomeToScene;
        SceneManager.sceneLoaded -= LogSceneDetails;
    }

    //changed method name for readability
    private void LogSceneDetails(Scene loadedScene, LoadSceneMode loadMode)
    {
        Debug.Log($"Scene Loaded: Index - {loadedScene.buildIndex}, Name - {loadedScene.name}");
    }

    //changed method name for readability
    private void WelcomeToScene(Scene loadedScene, LoadSceneMode loadMode)
    {
        Debug.Log("Hello. Welcome to my scene.");
    }

    #endregion

    #region Scene Loading
    //created simplified method to load scene (called in Start())
    private void LoadInitialScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    #endregion
}
