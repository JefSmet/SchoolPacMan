using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan;
using QuestMan.Singleton;
using QuestMan.Observer;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
{
    
    public GameObject hudControllerPrefab;
    public GameObject arduinoControllerPrefab;
    private int currentStudiepunten;
    public HUDController hudController;
    SerialCommThreaded arduinoController;
    
    public void AddStudiepunt(Studiepunt punt)
    {
        currentStudiepunten += punt.Value;
        LevelManager.Instance.RemoveStudiepunt(punt);        
        hudController.UpdateStudiepunten(currentStudiepunten);
    }

    void Start()
    {
        currentStudiepunten = 0;
        hudControllerPrefab = GameObject.Instantiate(hudControllerPrefab);
        arduinoControllerPrefab = GameObject.Instantiate(arduinoControllerPrefab);
        hudController = hudControllerPrefab.GetComponent<HUDController>();
        arduinoController=arduinoControllerPrefab.GetComponent<SerialCommThreaded>();
    }
}
