using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan.Singleton;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    public int studiepuntenCount;
    private List<GameObject> checkPoints = new List<GameObject>();
    public List<GameObject> CheckPoints{get { return checkPoints; }}


     void Awake()
    {
        Instance.CheckPoints.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint")); 
    }



}
