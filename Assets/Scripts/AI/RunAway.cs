using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : State
{
    int currentIndex = 0;    
    public RunAway(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.RUNAWAY;
        float biggestDistance = 0;
        for (int i = 0; i < LevelManager.Instance.RunAwayPoints.Count; i++)
        {
            GameObject thisCheckpoint = LevelManager.Instance.RunAwayPoints[i];
            float distance = Vector3.Distance(npc.transform.position, thisCheckpoint.transform.position);
            if (distance > biggestDistance)
            {
                currentIndex = i;
                biggestDistance = distance;
            }
        }
    }

    public override void Enter()
    {             
        if (anim != null)
        {
            anim.SetTrigger("isRunning");
        }      
        agent.speed = 15;
        agent.isStopped = false;
        agent.SetDestination(LevelManager.Instance.RunAwayPoints[currentIndex].transform.position);
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (agent.remainingDistance < 1)
        {
            nextState = new Idle(npc, agent, anim, player, audioSource);
            stage = EVENT.EXIT;
        }
    }
    public override void Exit()
    {
        if (anim != null)
        {
            anim.ResetTrigger("isRunning");
        }
        base.Exit();
    }
}
