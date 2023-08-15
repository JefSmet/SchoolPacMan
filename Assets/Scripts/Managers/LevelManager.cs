using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using StarterAssets;
using System.Collections;

public class LevelManager : QuestMan.Singleton.Singleton<LevelManager>
{
    [SerializeField] private int ballValue = 5;
    [SerializeField] private Color ballColor = Color.green;
    [SerializeField] private int superballValue = 10;
    [SerializeField] private Color superballColor = Color.red;
    [SerializeField] private float percentageSuperballs = 20f;
    private List<GameObject> patrolpoints = new List<GameObject>();
    private List<GameObject> runAwayPoints = new List<GameObject>();
    private List<GameObject> hidingPoints = new List<GameObject>();
    private List<Studiepunt> studiepunten = new List<Studiepunt>();
    private List<GameObject> agents = new List<GameObject>();
    private GameObject player;
    private FirstPersonController fpc;
    private Transform aiSpawn;
    private Transform playerSpawn;

    public List<GameObject> PatrolPoints { get { return patrolpoints; } }
    public List<Studiepunt> Studiepunten { get { return studiepunten; } }

    public List<GameObject> RunAwayPoints { get { return runAwayPoints; } }
    public List<GameObject> HidingPoints { get { return hidingPoints; } }
    public int LevelScore { get; set; }
    public float PlayerMoveSpeed { get { return fpc.MoveSpeed; } }
    void Start()
    {
        aiSpawn = GameObject.FindGameObjectWithTag("AISpawn").transform;
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        patrolpoints.AddRange(GameObject.FindGameObjectsWithTag("PatrolPoint"));
        runAwayPoints.AddRange(GameObject.FindGameObjectsWithTag("RunAwayAI"));
        hidingPoints.AddRange(GameObject.FindGameObjectsWithTag("HidingPoint"));
        studiepunten.AddRange(FindObjectsOfType<Studiepunt>());
        InitializeBalls();
        RandomizeSuperballs();
        GameManager.Instance.HudController.SetSPText(0);
        GameManager.Instance.HudController.SetBallsToGoText(GetActiveStudiepunten());
        GameManager.Instance.HudController.SetLivesText(3);
        agents.AddRange(GameObject.FindGameObjectsWithTag("AI"));
        player = GameObject.FindGameObjectWithTag("Player");
        fpc = player.GetComponent<FirstPersonController>();
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
        fpc.enabled = false;
        StartCoroutine(respawnDelay());
        player.transform.position = playerSpawn.position;
    }

    IEnumerator respawnDelay()
    {
        yield return new WaitForSeconds(0.1f);
        fpc.enabled = true;
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
