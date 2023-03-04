using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        IDLE, PURSUE, BOB, PATROL
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

    float visDist = 10f;
    float visAngle = 30f;
    
    public State(GameObject npc, NavMeshAgent agent, Animator anim, Transform player)
    {
        this.npc = npc;
        this.anim = anim;
        this.agent = agent;
        this.player = player;
        this.stage = EVENT.ENTER;        
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
       switch(stage)
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
        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.transform.forward- npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        return (direction.magnitude < visDist && angle < visAngle);        
    }
}
