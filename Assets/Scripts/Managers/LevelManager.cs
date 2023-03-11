using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan.Singleton;
using System.Runtime.CompilerServices;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
    private List<GameObject> waypoints = new List<GameObject>();
    private Dictionary<Studiepunt,int> studiepunten = new Dictionary<Studiepunt,int>();
    private int total;
    public List<GameObject> Waypoints{get { return waypoints; }}
    public Dictionary<Studiepunt,int> Studiepunten { get { return studiepunten;} }
    public int StudiepuntenValues {
        get 
        { 
            total= 0;
            foreach(int values in studiepunten.Values)
            {
                total+= values;
            }
            return total ; 
        } 
    }
    public int StudieBallenCount { get { return studiepunten.Count; } }

    public void RemoveStudiepunt(Studiepunt studiepunt)
    {
        studiepunten.Remove(studiepunt);
        Destroy(studiepunt.gameObject);
        //if (!studiepunten.Remove(studiepunt))
        //    Debug.Log(studiepunt.gameObject.name + " is not removed");
        //Destroy(studiepunt.gameObject);

    }


    void Awake()
    {
        Studiepunt[] studiepuntenArray = FindObjectsOfType<Studiepunt>();
        waypoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
        foreach (Studiepunt sp in studiepuntenArray)
        {
            studiepunten.Add(sp,sp.Value) ;
        }
    }


}
