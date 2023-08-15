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
        set { lives = value; if (lives > -1) _hudController.SetLivesText(lives); else { Defeat(); } }
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
                _levelManager= gameObject.AddComponent<LevelManager>();
            }
            return _levelManager; 
        } 
    }

    private void Start()
    {
        FirstPersonController fpc = GameObject.FindObjectOfType<FirstPersonController>();
        if (fpc != null) 
        {
            _hudController.SetPlayerSpeedText(fpc.MoveSpeed);
        }
        Lives = 3;
    }

    public void LoadNextLevel()
    {
        HudController.gameObject.SetActive(false);
        SceneManager.LoadScene("NextLevel");
    }
}
