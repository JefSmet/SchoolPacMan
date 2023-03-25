using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursueDoctor : State
{
    public PursueDoctor(GameObject npc, NavMeshAgent agent, Animator anim, UnityEngine.Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
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
        Seek(player.position); ;
        if (agent.hasPath)
        {
            if (CanAttackPlayer()&&!PlayerCanSeeNpc())
            {
                nextState = new AttackDoctor(npc, agent, anim, player, audioSource);
                stage = EVENT.EXIT;
                return;
            }
            else
            {
                if (!CanSeePlayer())
                {
                    nextState = new PatrolDoctor(npc, agent, anim, player, audioSource);
                    stage = EVENT.EXIT;                    
                }
            }
        }
    }
}
