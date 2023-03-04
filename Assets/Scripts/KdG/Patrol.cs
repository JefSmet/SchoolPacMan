using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    int currentIndex = -1;

    public Patrol(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.PATROL;
        agent.speed = 2;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        currentIndex= 0;
        if (anim != null)
        {
            anim.SetTrigger("isWalking");
        }
        agent.isStopped = false;
        base.Enter();
    }

    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            if (currentIndex >= LevelManager.Instance.CheckPoints.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
            //currentIndex = (currentIndex + 1) % LevelManager.Instance.CheckPoints.Count;
            agent.SetDestination(LevelManager.Instance.CheckPoints[currentIndex].transform.position);
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
