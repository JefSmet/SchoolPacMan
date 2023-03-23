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
    }

    public override void Enter()
    {
        base.Enter();
        if (anim != null)
        {
            anim.SetTrigger("isWalking");
        }
        agent.speed = 3;
        agent.isStopped = false;        
    }

    public override void Update()
    {
        base.Update();
        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, anim, player, audioSource);
            stage = EVENT.EXIT;
        }
        else if (agent.remainingDistance < 1 && !agent.pathPending)
        {
            //currentIndex = (currentIndex + 1) % LevelManager.Instance.Waypoints.Count;
            currentIndex= Random.Range(0, LevelManager.Instance.PatrolPoints.Count-1);
            agent.SetDestination(LevelManager.Instance.PatrolPoints[currentIndex].transform.position);
        }  
        else if (IsPlayerBehind())
        {
            nextState = new RunAway(npc, agent, anim, player, audioSource);
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
