using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    int currentIndex = 0;

    public Patrol(GameObject npc, NavMeshAgent agent, Animator anim, UnityEngine.Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.PATROL;
        agent.speed = 3;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        //float smallestDistance = Mathf.Infinity;
        //for (int i = 0; i < LevelManager.Instance.PatrolPoints.Count; i++)
        //{
        //    GameObject thisCheckpoint = LevelManager.Instance.PatrolPoints[i];
        //    float distance = Vector3.Distance(npc.transform.position, thisCheckpoint.transform.position);
        //    if (distance < smallestDistance)
        //    {
        //        currentIndex = i - 1; // currentIndex gets incremented in Update(), so deduct one here
        //        smallestDistance = distance;
        //    }
        //}
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
