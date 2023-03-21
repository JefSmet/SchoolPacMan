using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pursue : State
{
    public Pursue(GameObject npc, NavMeshAgent agent, Animator anim, UnityEngine.Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.PURSUE;
        agent.speed = 5;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        if (anim != null) 
        {
            anim.SetTrigger("isRunning");
        }
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position); ;
        if (agent.hasPath)
        {
            if (CanAttackPlayer())
            {
                nextState = new Attack(npc, agent, anim, player, audioSource);
                stage = EVENT.EXIT;
            }
            else
            {
                if (!CanSeePlayer())
                {
                    nextState = new Patrol(npc, agent, anim, player, audioSource);
                    stage = EVENT.EXIT;
                }
            }
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
