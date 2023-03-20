using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    float rotationSpeed = 2f;

    public Attack(GameObject npc, NavMeshAgent agent, Animator anim, UnityEngine.Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.ATTACK;
    }

    public override void Enter()
    {
        if (anim!= null) 
        {
            anim.SetTrigger("isAttacking");
        }
        agent.isStopped= true;
        if (audioSource!= null)
        {
            audioSource.Play();
        }        
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        Vector3 direction = player.position-npc.transform.position;
        float angle = Vector3.Angle(direction,npc.transform.forward);
        direction.y = 0;

        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,Quaternion.LookRotation(direction), Time.deltaTime*rotationSpeed);

        if (!CanAttackPlayer())
        {
            nextState = new Idle (npc, agent, anim, player,audioSource);
            stage = EVENT.EXIT;
        }
    }
    public override void Exit() 
    {
        if(anim!= null)
        {
            anim.ResetTrigger("isAttacking");
        }
        if (audioSource!=null)
        {
            audioSource.Stop();
        }
        base.Exit();
    }
}
