using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private State currentState;
    private AudioSource audioSource; 
    
    public Transform player;

    // Start is called before the first frame update
    private void Start()
    {
        agent= GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource= GetComponent<AudioSource>();
        currentState = new Idle(gameObject, agent, animator, player, audioSource);
    }

    // Update is called once per frame
    private void Update()
    {
        currentState = currentState.Process();
    }
}
