using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan.Singleton;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using System.Linq;

public class LevelManager : QuestMan.Singleton.Singleton<LevelManager>
{
    [SerializeField] int ballValue = 5;
    [SerializeField] Color ballColor = Color.green;

    [SerializeField] int superballValue = 10;
    [SerializeField] Color superballColor = Color.red;
    [SerializeField] float percentageSuperballs = 20f;
    
    List<PatrolPoint> _patrolpoints = new List<PatrolPoint>();
    List<Studiepunt> _studiepunten = new List<Studiepunt>();
    public List<PatrolPoint> PatrolPoints { get { return _patrolpoints; } }
    public List<Studiepunt> Studiepunten { get { return _studiepunten;} }
    public int LevelScore { get; set; }

    public void StudiepuntPickedUp(Studiepunt sp)
    {
       //int i = _studiepunten.IndexOf(sp);
       // if (i > -1)
       // {
            //_studiepunten[i].gameObject.SetActive(false);
            sp.gameObject.SetActive(false);
            LevelScore += sp.Value;
            GameManager.Instance.GameScore += sp.Value;
            GameManager.Instance.HudController.SetSPText(LevelScore);
            GameManager.Instance.HudController.SetBallsToGoText(GetActiveStudiepunten());
            GameManager.Instance.ArduinoController.Blink();
        //}        
    }

    int GetActiveStudiepunten()
    {
        return _studiepunten.Where(sp => sp.gameObject.activeInHierarchy).Count();
    }

    void InitializeBalls()
    {
        foreach (Studiepunt sp in _studiepunten)
        {
            ChangeBallValueColor(sp, ballValue, ballColor);
        }
    }

    void RandomizeSuperballs()
    {
         int superballCount = Mathf.RoundToInt( percentageSuperballs / 100 * _studiepunten.Count() );

        for (int i = 0; i < superballCount; i++)
        {
            int index = Random.Range(0,_studiepunten.Count());
            while (_studiepunten[index].Value==superballValue)
            {
                index = Random.Range(0, _studiepunten.Count());
            }
            ChangeBallValueColor(_studiepunten[index], superballValue, superballColor);
        }
    }

    void ChangeBallValueColor(Studiepunt studiepunt, int value , Color color)
    {
        studiepunt.Value = value;
        Renderer rend = studiepunt.GetComponent<Renderer>();
        rend.material.color = color;
    }

    void Start()
    {        
        _patrolpoints.AddRange(FindObjectsOfType<PatrolPoint>());
        _studiepunten.AddRange(FindObjectsOfType<Studiepunt>());
        GameManager.Instance.HudController.SetBallsToGoText(GetActiveStudiepunten());
        InitializeBalls();
        RandomizeSuperballs();    
    }


}
