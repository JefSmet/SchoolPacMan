using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan;
using QuestMan.Singleton;
using QuestMan.Observer;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
{
    public ScoreController scoreController;
    public GameObject hudController;
    
    public void AddScore(int score)
    {
        scoreController.AddStudiepunten(score);        
    }

    void Start()
    {
        GameObject.Instantiate(hudController);
        if (Instance.scoreController == null)
        {
            scoreController = gameObject.AddComponent<ScoreController>();
        }
        if (Instance.hudController == null)
        {
              
        }
    }
}
