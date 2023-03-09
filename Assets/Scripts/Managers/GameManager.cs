using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan;
using QuestMan.Singleton;
using QuestMan.Observer;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
{
    
    public GameObject hudControllerPrefab;
    private int currentStudiepunten;
    HUDController hudController;
    
    public void AddStudiepunten(Studiepunt punt)
    {
        currentStudiepunten += punt.Value;
        LevelManager.Instance.Studiepunten.Remove(punt);
        Destroy(punt.gameObject);
        hudController.UpdateStudiepunten(currentStudiepunten);
    }

    void Start()
    {
        currentStudiepunten = 0;
        hudControllerPrefab = GameObject.Instantiate(hudControllerPrefab);
        hudController= hudControllerPrefab.GetComponent<HUDController>();
        
    }
}
