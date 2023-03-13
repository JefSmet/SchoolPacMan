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
        agent.speed = 3;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        float lastDistance = Mathf.Infinity;
        for (int i = 0; i < LevelManager.Instance.PatrolPoints.Count; i++)
        {
            PatrolPoint thisCheckpoint = LevelManager.Instance.PatrolPoints[i];
            float distance = Vector3.Distance(npc.transform.position, thisCheckpoint.gameObject.transform.position);
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
        base.Update();        
        if (agent.remainingDistance < 1 && !agent.pathPending)
        {
            //currentIndex = (currentIndex + 1) % LevelManager.Instance.Waypoints.Count;
            currentIndex= Random.Range(0, LevelManager.Instance.PatrolPoints.Count-1);
            agent.SetDestination(LevelManager.Instance.PatrolPoints[currentIndex].transform.position);
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
