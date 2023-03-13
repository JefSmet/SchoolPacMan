using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan.Singleton;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using System.Linq;

public class LevelManager : QuestMan.Singleton.Singleton<LevelManager>
{
    List<PatrolPoint> _patrolpoints = new List<PatrolPoint>();
    List<Studiepunt> _studiepunten = new List<Studiepunt>();
    public List<PatrolPoint> PatrolPoints { get { return _patrolpoints; } }
    public List<Studiepunt> Studiepunten { get { return _studiepunten;} }
    public int LevelScore { get; set; }

    public void StudiepuntPickedUp(Studiepunt sp)
    {
       int i = _studiepunten.IndexOf(sp);
        if (i > -1)
        {
            _studiepunten[i].gameObject.SetActive(false);
            LevelScore += sp.Value;
            GameManager.Instance.GameScore += sp.Value;
            GameManager.Instance.HudController.SetSPText(LevelScore);
            GameManager.Instance.HudController.SetBallsToGoText(GetActiveStudiepunten());
            GameManager.Instance.ArduinoController.Blink();
        }        
    }

    int GetActiveStudiepunten()
    {
        return _studiepunten.Where(sp => sp.gameObject.activeInHierarchy).Count();
    }

    void Start()
    {        
        _patrolpoints.AddRange(FindObjectsOfType<PatrolPoint>());
        _studiepunten.AddRange(FindObjectsOfType<Studiepunt>());
        GameManager.Instance.HudController.SetBallsToGoText(GetActiveStudiepunten());
    }


}
