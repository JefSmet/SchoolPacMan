using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using StarterAssets;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class LevelManager : QuestMan.Singleton.Singleton<LevelManager>
{
    [SerializeField] int ballValue = 5;
    [SerializeField] Color ballColor = Color.green;
    [SerializeField] int superballValue = 10;
    [SerializeField] Color superballColor = Color.red;
    [SerializeField] float percentageSuperballs = 20f;
    List<GameObject> patrolpoints = new List<GameObject>();
    
    List<GameObject> hidingPoints = new List<GameObject>();
    List<Studiepunt> studiepunten = new List<Studiepunt>();
    List<GameObject> agents = new List<GameObject>();
    GameObject player;
    TeleportationProvider teleportationProvider;
    LocomotionSystem locomotionSystem;
    ActionBasedSnapTurnProvider snapTurnProvider;
    Transform aiSpawn;
    Transform playerSpawn;

    public List<GameObject> PatrolPoints { get { return patrolpoints; } }
    public List<Studiepunt> Studiepunten { get { return studiepunten; } }

    
    public List<GameObject> HidingPoints { get { return hidingPoints; } }
    public int LevelScore { get; set; }
    //public float PlayerMoveSpeed { get { return fpc.MoveSpeed; } }
    void Start()
    {
        aiSpawn = GameObject.FindGameObjectWithTag("AISpawn").transform;
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        patrolpoints.AddRange(GameObject.FindGameObjectsWithTag("PatrolPoint"));
        hidingPoints.AddRange(GameObject.FindGameObjectsWithTag("HidingPoint"));
        studiepunten.AddRange(FindObjectsOfType<Studiepunt>());
        InitializeBalls();
        RandomizeSuperballs();
        agents.AddRange(GameObject.FindGameObjectsWithTag("AI"));
        player = GameObject.FindGameObjectWithTag("Player");
        //fpc = player.GetComponent<FirstPersonController>();
        locomotionSystem= GetComponent<LocomotionSystem>();
        teleportationProvider= GetComponent<TeleportationProvider>();
        snapTurnProvider = GetComponent<ActionBasedSnapTurnProvider>();
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
        //fpc.enabled = false;
        locomotionSystem.enabled= false;
        teleportationProvider.enabled= false;
        snapTurnProvider.enabled= false;
        StartCoroutine(respawnDelay());
        player.transform.position = playerSpawn.position;
    }

    IEnumerator respawnDelay()
    {        
        yield return new WaitForSeconds(0.1f);
        //fpc.enabled = true;
        locomotionSystem.enabled= true;
        teleportationProvider.enabled= true;
        snapTurnProvider.enabled= true;
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
        GameManager.Instance.ArduinoController.Blink(GetStudiepuntLight(sp));
        if (GetActiveStudiepunten() == 0)
        {
            GameManager.Instance.LoadNextLevel();
        }
    }


    private void SwitchBalls()
    {
        foreach (Studiepunt sp in studiepunten.Where(sp => sp.gameObject.activeInHierarchy))
        {
            if (sp.Value == ballValue)
            {
                ChangeBallValueColor(sp, superballValue, superballColor);
            }
            else
            {
                ChangeBallValueColor(sp, ballValue, ballColor);
            }
        }
    }

    ArduinoLight GetStudiepuntLight(Studiepunt sp)
    {
        if (sp.Value == ballValue)
        {
            return ArduinoLight.GreenLight;
        }
        return ArduinoLight.RedLight;
    }

    int GetActiveStudiepunten()
    {
        return studiepunten.Where(sp => sp.gameObject.activeInHierarchy).Count();
    }

    void InitializeBalls()
    {
        foreach (Studiepunt sp in studiepunten)
        {
            ChangeBallValueColor(sp, ballValue, ballColor);
        }
    }

    void RandomizeSuperballs()
    {
        int superballCount = Mathf.RoundToInt(percentageSuperballs / 100 * studiepunten.Count());

        for (int i = 0; i < superballCount; i++)
        {
            int index = Random.Range(0, studiepunten.Count());
            while (studiepunten[index].Value == superballValue)
            {
                index = Random.Range(0, studiepunten.Count());
            }
            ChangeBallValueColor(studiepunten[index], superballValue, superballColor);
        }
    }

    void ChangeBallValueColor(Studiepunt studiepunt, int value, Color color)
    {
        studiepunt.Value = value;
        Renderer rend = studiepunt.GetComponent<Renderer>();
        rend.material.color = color;
    }
}
