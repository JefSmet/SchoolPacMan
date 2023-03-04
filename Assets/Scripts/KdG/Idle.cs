using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{

    public Idle(GameObject npc, NavMeshAgent agent, Animator anim, Transform player) : base(npc, agent, anim, player)
    {
        name = STATE.IDLE;
    }


    public override void Enter()
    {
        if (anim != null)
            anim.SetTrigger("idle");
        base.Enter();
    }

    public override void Update() 
    {
        if (Random.Range(0,100)<10) 
        {
            nextState = new Patrol(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
       
    }

    public override void Exit() 
    {
        if (anim != null)
            anim.ResetTrigger("idle");
        base.Exit();
    }
}
