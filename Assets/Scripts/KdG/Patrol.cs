using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    int currentIndex = -1;


    public Patrol(GameObject npc, NavMeshAgent agent, Animator anim, Transform player) : base(npc, agent, anim, player)
    {
        name = STATE.PATROL;
        agent.speed= 2;
        agent.isStopped= false;
    }

    public override void Enter()
    {
        currentIndex= 0;
        anim.SetTrigger("isWalking");
        base.Enter();
    }

    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            //if(currentIndex >= LevelManager.Instance.CheckPoints.Count - 1)
            //{
            //    currentIndex = 0;
            //}
            //else
            //{
            //    currentIndex++;
            //}
            currentIndex = (currentIndex + 1) % LevelManager.Instance.CheckPoints.Count;
            agent.SetDestination(LevelManager.Instance.CheckPoints[currentIndex].transform.position);
        }
        base.Update();
    }
    public override void Exit() 
    {
        anim.ResetTrigger("isWalking");
        base.Exit();
    }
}
