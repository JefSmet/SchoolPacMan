using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        IDLE, PURSUE, BOB, PATROL, ATTACK
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;
    protected AudioSource audioSource;

    float visDist = 10.0f;
    float visAngle = 30.0f;
    float attackDistance = 7.0f;
    
    public State(GameObject npc, NavMeshAgent agent, Animator anim, Transform player,AudioSource audioSource)
    {
        this.npc = npc;
        this.anim = anim;
        this.agent = agent;
        this.player = player;
        this.stage = EVENT.ENTER;        
        this.audioSource = audioSource;
    }


    public virtual void Enter()
    {
        stage=EVENT.UPDATE;
    }
    public virtual void Update() 
    {
        stage=EVENT.UPDATE;
    }
    public virtual void Exit()
    {
        stage=EVENT.EXIT;
    }

    public State Process()
    {
        switch (stage)
        {
            case EVENT.ENTER:
                Enter();
                break;
            case EVENT.UPDATE:
                Update();
                break;
            case EVENT.EXIT:
                Exit();
                return nextState;
        }
        //if (stage==EVENT.ENTER) { Enter(); };
        //if (stage==EVENT.UPDATE) { Update(); };
        //if (stage==EVENT.EXIT) { Exit(); return nextState; };        
        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.transform.position- npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);


        if ((direction.magnitude < visDist) && (angle < visAngle))
        {           
            LayerMask mask = LayerMask.GetMask("Wall");
            // Check if a Wall is hit.
            if (Physics.Raycast(npc.transform.position, npc.transform.forward, direction.magnitude, mask))
            {
                return false;
            }

            return true;
        }
        return false;
        //return ((direction.magnitude < visDist) && (angle < visAngle));
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.transform.position - npc.transform.position;
        //if (direction.magnitude < attackDistance)
        //{
        //    return true;
        //}
        //return false;
        return direction.magnitude < attackDistance;
    }
}
