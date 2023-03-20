using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan;
using QuestMan.Singleton;
using StarterAssets;

public class GameManager : SingletonPersistent<GameManager>
{
    [SerializeField]GameObject _hudControllerPrefab;
    [SerializeField]GameObject _arduinoControllerPrefab;
    [SerializeField]LevelManager _levelManager;
    SerialCommThreaded _arduinoController;
    HUDController _hudController;
    public int GameScore { get; set; }
    private int lives;

    public int Lives
    {
        get { return lives; }
        set { lives = value; if (lives > -1) _hudController.SetLivesText(Lives); }
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
}
