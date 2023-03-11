using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan;
using QuestMan.Singleton;
using QuestMan.Observer;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
{
    public SerialCommThreaded arduinoController;
    public HUDController hudController;
    GameObject hudControllerPrefab;
    GameObject arduinoControllerPrefab;
    int currentStudiepunten;    
   
    
    public void AddStudiepunt(Studiepunt punt)
    {
        currentStudiepunten += punt.Value;
        LevelManager.Instance.RemoveStudiepunt(punt);        
        hudController.UpdateStudiepunten(currentStudiepunten);
    }

    void Start()
    {
        currentStudiepunten = 0;
        hudControllerPrefab = Instantiate(hudControllerPrefab);
        arduinoControllerPrefab = Instantiate(arduinoControllerPrefab);
        hudController = hudControllerPrefab.GetComponent<HUDController>();
        arduinoController=arduinoControllerPrefab.GetComponent<SerialCommThreaded>();
    }
}
