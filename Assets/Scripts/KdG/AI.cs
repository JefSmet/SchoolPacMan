using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    public Transform player;
    State currentState;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource= GetComponent<AudioSource>();
        currentState = new Idle(gameObject, agent, animator, player,audioSource);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
}
