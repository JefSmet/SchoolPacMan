using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursueSanta : State
{
    public PursueSanta(GameObject npc, NavMeshAgent agent, Animator anim, UnityEngine.Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.PURSUE;

    }

    public override void Enter()
    {
        base.Enter();
        agent.speed = 10;
        agent.isStopped = false;
    }

    public override void Update()
    {
        if (agent.hasPath)
        {
            if (CanAttackPlayer())
            {
                nextState = new AttackSanta(npc, agent, anim, player, audioSource);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer())
            {
                nextState = new WanderSanta(npc, agent, anim, player, audioSource);
                stage = EVENT.EXIT;
            }
        }
        else
        {
            Seek(player.position);
        }
    }
}
