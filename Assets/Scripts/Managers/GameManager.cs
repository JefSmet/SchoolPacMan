using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan;
using QuestMan.Singleton;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
{
    public int score;


    public void AddScore(int score)
    {
        this.score += score;
    }

   
}
