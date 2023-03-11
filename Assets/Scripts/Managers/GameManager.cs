using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan;
using QuestMan.Singleton;
using QuestMan.Observer;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
{
    SerialCommThreaded arduinoController;
    HUDController hudController;
    [SerializeField]GameObject hudControllerPrefab;
    [SerializeField]GameObject arduinoControllerPrefab;
    int currentStudiepunten;

    public HUDController HudController{ get=> hudController; }
    public SerialCommThreaded ArduinoController { get =>arduinoController;}
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
