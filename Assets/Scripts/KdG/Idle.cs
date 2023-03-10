using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    public Idle(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        if (anim != null)
        {
            anim.SetTrigger("isIdle");
        }
        agent.isStopped = true;
        base.Enter();
    }

    public override void Update() 
    {
        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent,anim,player,audioSource);
            stage = EVENT.EXIT;
        }
        else if (Random.Range(0,100)<10) 
        {
            nextState = new Patrol(npc, agent, anim, player,audioSource);
            stage = EVENT.EXIT;
        }
       
    }

    public override void Exit() 
    {
        if (anim != null)
        {
            anim.ResetTrigger("isIdle");

        }
        base.Exit();
    }
}
