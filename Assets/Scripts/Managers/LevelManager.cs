using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestMan.Singleton;
using System.Runtime.CompilerServices;

public class LevelManager : MonoBehaviourSingleton<LevelManager>
{
<<<<<<< Updated upstream
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
=======
    [SerializeField] int ballValue = 5;
    [SerializeField] Color ballColor = Color.green;

    [SerializeField] int superballValue = 10;
    [SerializeField] Color superballColor = Color.red;
    [SerializeField] float percentageSuperballs = 20f;
    private Transform aiSpawn;
    private Transform playerSpawn;

    List<GameObject> _patrolpoints = new List<GameObject>();
    List<Studiepunt> _studiepunten = new List<Studiepunt>();
    List<GameObject> agents = new List<GameObject>();
    private GameObject player;

    public List<GameObject> PatrolPoints { get { return _patrolpoints; } }
    public List<Studiepunt> Studiepunten { get { return _studiepunten; } }
    public int LevelScore { get; set; }
    void Start()
    {
        aiSpawn = GameObject.FindGameObjectWithTag("AISpawn").transform;
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        _patrolpoints.AddRange(GameObject.FindGameObjectsWithTag("PatrolPoint"));
        _studiepunten.AddRange(FindObjectsOfType<Studiepunt>());
        InitializeBalls();
        RandomizeSuperballs();
        GameManager.Instance.HudController.SetSPText(0);
        GameManager.Instance.HudController.SetBallsToGoText(GetActiveStudiepunten());
        GameManager.Instance.HudController.SetLivesText(3);
        agents.AddRange(GameObject.FindGameObjectsWithTag("AI"));
        player = GameObject.FindGameObjectWithTag("Player");
        RespawnPlayer();
        RespawnAgents();

    }


    public void Die()
    {
        GameManager.Instance.Lives -= 1;
        RespawnPlayer();
        RespawnAgents();
       
    }

    private void RespawnPlayer()
    {
        player.transform.position = playerSpawn.position;        
    }

    private void RespawnAgents()
    {

        foreach (GameObject agent in agents)
        {
            NavMeshAgent nma = agent.GetComponent<NavMeshAgent>();
            nma.enabled = false;
            agent.transform.position = aiSpawn.position;
            nma.enabled = true;
        }
    }

    private void OnEnable()
    {
        SerialCommThreaded.onButtonPressed += SwitchBalls;
    }

    private void OnDisable()
    {
        SerialCommThreaded.onButtonPressed -= SwitchBalls;
    }

    public void StudiepuntPickedUp(Studiepunt sp)
    {
        sp.gameObject.SetActive(false);
        LevelScore += sp.Value;
        GameManager.Instance.GameScore += sp.Value;
        GameManager.Instance.HudController.SetSPText(LevelScore);
        GameManager.Instance.HudController.SetBallsToGoText(GetActiveStudiepunten());
        GameManager.Instance.ArduinoController.Blink(GetStudiepuntLight(sp));
        if (GetActiveStudiepunten() == 0)
        {
            GameManager.Instance.LoadNextLevel();
        }
    }


    private void SwitchBalls()
    {
        foreach (Studiepunt sp in _studiepunten.Where(sp => sp.gameObject.activeInHierarchy))
        {
            if (sp.Value == ballValue)
>>>>>>> Stashed changes
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
