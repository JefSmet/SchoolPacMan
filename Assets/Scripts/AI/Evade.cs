using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Evade : State
{
    float distancePlayerStopEvade = 3.0f;
    public Evade(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.EVADE;
        agent.speed = 5;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        base.Enter();        
        if (anim != null)
        {
            anim.SetTrigger("isWalking");
        }        
    }

    public override void Update()
    {
        base.Update();
        if (Vector3.Distance(player.position, npc.transform.position)>distancePlayerStopEvade)
        {
            nextState = new Idle(npc, agent, anim, player, audioSource);
            stage = EVENT.EXIT;
            return;
        }

        Vector3 targetDir = player.position - npc.transform.position;
        float lookAhead = targetDir.magnitude / (agent.speed + 1);

        //same as pursue but instead of seek we are fleeing
        Flee(player.position + player.forward * lookAhead);
    }

    public override void Exit() 
    { 
        base.Exit();
        if (anim != null)
        {
            anim.ResetTrigger("isWalking");
        }
    }
}
