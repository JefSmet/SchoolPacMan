using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursueCop : State
{
    public PursueCop(GameObject npc, NavMeshAgent agent, Animator anim, UnityEngine.Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.PURSUE;
        
    }

    public override void Enter()
    {
        base.Enter();
        agent.speed = 5;
        agent.isStopped = false;
    }

    public override void Update()
    {
        agent.SetDestination(player.position); ;
        if (agent.hasPath)
        {
            if (CanAttackPlayer())
            {
                nextState = new AttackCop(npc, agent, anim, player, audioSource);
                stage = EVENT.EXIT;
            }
            else
            {
                if (!CanSeePlayer())
                {
                    nextState = new PatrolCop(npc, agent, anim, player, audioSource);
                    stage = EVENT.EXIT;
                }
            }
        }
    }
}
