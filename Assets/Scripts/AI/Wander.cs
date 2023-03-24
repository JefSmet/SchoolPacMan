using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : State
{    
    float wanderRadius = 10;
    float wanderDistance = 10;
    float wanderJitter = 1;
    Vector3 wanderTarget = Vector3.zero;
    public Wander(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.WANDER;
        agent.speed = 2f;
    }

    public override void Enter()
    {
        base.Enter();
        if (anim!= null)
        {
            anim.SetTrigger("isWalking");
        }
    }

    public override void Update()
    {
        base.Update();
       
        //determine a location on a circle 
        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
                                        0,
                                        Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        //project the point out to the radius of the cirle
        wanderTarget *= wanderRadius;

        //move the circle out in front of the agent to the wander distance
        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        //work out the world location of the point on the circle.
        Vector3 targetWorld = npc.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
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
