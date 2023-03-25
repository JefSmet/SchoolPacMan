using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiCop : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    public Transform player;
    State currentState;
    AudioSource audioSource;
    //bool arduinoButtonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentState = new IdleCop(gameObject, agent, animator, player, audioSource);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Studiepunt")
        {
            LevelManager.Instance.RespawnAgent(gameObject);
            AudioManager.Instance.audioSource.clip = audioSource.clip;
            AudioManager.Instance.audioSource.Play();
        }
    }
}