using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan;
using QuestMan.Singleton;
using StarterAssets;
using System;
using UnityEngine.SceneManagement;

public class GameManager : SingletonPersistent<GameManager>
{
    [SerializeField] private GameObject _hudControllerPrefab;
    [SerializeField] private GameObject _arduinoControllerPrefab;
    [SerializeField] private LevelManager _levelManager;
    private SerialCommThreaded _arduinoController;
    private HUDController _hudController;
    private int lives;
    public int GameScore { get; set; }

    public int Lives
    {
        get { return lives; }
        set { lives = value; if (lives > -1) HudController.SetLivesText(lives); else { Defeat(); } }
    }

    private void Defeat()
    {
        SceneManager.LoadScene("Defeat");
    }

    public void Victory()
    {
        SceneManager.LoadScene("Victory");
    }

    public HUDController HudController
    { 
        get 
        {  
            if (_hudController == null)
            {                
                if (_hudControllerPrefab == null)
                {
                    _hudController = gameObject.AddComponent<HUDController>();
                }
                else
                {
                    _hudController = _hudControllerPrefab.GetComponent<HUDController>();
                }
            }   
            return _hudController;
        }
    }
    public SerialCommThreaded ArduinoController 
    {
        get
        {
            if (_arduinoController == null)
            {
                if (_arduinoControllerPrefab == null)
                {
                    _arduinoController = gameObject.AddComponent<SerialCommThreaded>();
                }
                else
                {
                    _arduinoController = _arduinoControllerPrefab.GetComponent<SerialCommThreaded>();
                }
            }
            return _arduinoController;
        }
    }
    public LevelManager LevelManager 
    {
        get 
        {
            if (_levelManager == null)
            {
                HudController.gameObject.SetActive(true);
                FirstPersonController fpc = GameObject.FindObjectOfType<FirstPersonController>();
                if (fpc != null)
                {
                    HudController.SetPlayerSpeedText(fpc.MoveSpeed);
                }
                _levelManager = FindObjectOfType<LevelManager>();
            }

            return _levelManager; 
        } 
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("BootScene"))
        {
            LoadSceneAfterDelay("StartScene");
        }
        
            
            Lives = 3;
        
        
    }

    public void LoadNextLevel()
    {
        HudController.gameObject.SetActive(false);
        SceneManager.LoadScene("NextLevel");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LevelDemo");        
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSceneAfterDelay(string sceneName)
    {
        StartCoroutine(DelayedSceneLoad(sceneName));
    }

    IEnumerator DelayedSceneLoad(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
}
