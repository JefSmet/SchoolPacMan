using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    int currentIndex = 0;

    public Patrol(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.PATROL;
        agent.speed = 2;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        float lastDistance = Mathf.Infinity;
        for (int i = 0; i < LevelManager.Instance.Waypoints.Count; i++)
        {
            GameObject thisCheckpoint = LevelManager.Instance.Waypoints[i];
            float distance = Vector3.Distance(npc.transform.position, thisCheckpoint.transform.position);
            if (distance < lastDistance)
            {
                currentIndex = i - 1; // currentIndex get incremented in Update(), so deduct one here
                lastDistance = distance;
            }
        }
        if (anim != null)
        {
            anim.SetTrigger("isWalking");
        }
        agent.isStopped = false;
        base.Enter();
    }

    public override void Update()
    {
        
        
        if (agent.remainingDistance < 1 && !agent.pathPending)
        {

            //if (currentIndex >= LevelManager.Instance.Waypoints.Count - 1)
            //{
            //    currentIndex = 0;
            //}
            //else
            //{
            //    currentIndex++;
            //}
            
            currentIndex = (currentIndex + 1) % LevelManager.Instance.Waypoints.Count;
            agent.SetDestination(LevelManager.Instance.Waypoints[currentIndex].transform.position);
        }
        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, anim, player, audioSource);
            stage = EVENT.EXIT;
        }
    }
    public override void Exit() 
    {
        if (anim != null)
        {
            anim.ResetTrigger("isWalking");
        }
        base.Exit();
    }
}
