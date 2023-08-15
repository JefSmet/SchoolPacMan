using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    private int currentIndex = 0;

    public Patrol(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.PATROL;        
    }

    public override void Enter()
    {
        base.Enter();
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
            currentIndex= Random.Range(0, LevelManager.Instance.PatrolPoints.Count-1);
            agent.SetDestination(LevelManager.Instance.PatrolPoints[currentIndex].transform.position);
        }  
        else if (IsPlayerBehind())
        {
            nextState = new 
                RunAway(npc, agent, anim, player, audioSource);
            stage = EVENT.EXIT;
        }
    }
    public override void Exit() 
    {
        base.Exit();
    }
}
