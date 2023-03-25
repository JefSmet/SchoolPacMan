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
    [SerializeField]GameObject _hudControllerPrefab;
    [SerializeField]GameObject _arduinoControllerPrefab;
    [SerializeField]LevelManager _levelManager;
    SerialCommThreaded _arduinoController;
    
    public int GameScore { get; set; }
    private int lives;

    public int Lives
    {
        get { return lives; }
        set { lives = value; if (lives < 0) Defeat() ; }
    }

    private void Defeat()
    {
        SceneManager.LoadScene("Defeat");
    }

    public void Victory()
    {
        SceneManager.LoadScene("Victory");
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
        
       
        Lives = 3;
    }

    public void LoadNextLevel()
    {
        
        SceneManager.LoadScene("NextLevel");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LevelDemo");
    }
}
