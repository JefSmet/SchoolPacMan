using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        IDLE, PURSUE, PATROL, ATTACK, RUNAWAY, FLEE, CLEVERPURSUE, EVADE, WANDER, CLEVERHIDE,
        HIDE
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

    private float visDist = 10.0f;
    private float visAngle = 67.5f;
    private float attackDistance = 2.0f;
    
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
        return this;
    }

    public void Seek(Vector3 location)
    {
        if(!agent.pathPending)
        {
            agent.isStopped = false;
            agent.SetDestination(location);
        }
    }

    public void Flee(Vector3 location)
    {
        if (!agent.pathPending)
        {
            agent.isStopped = false;
            //work out the vector away from the location
            //this is 180 degrees to the vector to the location
            Vector3 fleeVector = location - npc.transform.position;

            //take this vector away from the agent's position and 
            //set this as the new location on the nav mesh
            agent.SetDestination(npc.transform.position - fleeVector);
        }
    }

    public bool CanSeePlayer()
    {

        Vector3 direction = (player.position - npc.transform.position).normalized;
        float maxDistance = Vector3.Distance(player.position, npc.transform.position);


        float lookingAngle = Vector3.Angle(npc.transform.forward, direction);
        if ((maxDistance < visDist) && (lookingAngle < visAngle))
        {
            LayerMask mask = LayerMask.GetMask("Wall");
            RaycastHit hit;

            if (Physics.Raycast(npc.transform.position, direction, out hit, maxDistance, mask))
            {
                return false;
            }
            return true;
        }

        return false;
    }

    public bool IsPlayerBehind()
    {
        Vector3 direction = npc.transform.position - player.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        if ((direction.magnitude < 3) && (angle < 45))
        {            
            return true;
        }
        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        return direction.magnitude < attackDistance;
    }

    public bool PlayerCanSeeNpc()
    {        
        Vector3 direction = (npc.transform.position - player.position).normalized;
        float maxDistance = Vector3.Distance(player.position, npc.transform.position);


        float lookingAngle = Vector3.Angle(player.forward, direction);
        if (lookingAngle < 67.5f)
        {
            LayerMask mask = LayerMask.GetMask("Wall");
            RaycastHit hit;

            if (Physics.Raycast(player.position, direction, out hit, maxDistance,mask))
            {
                return false;
            }
            return true;
        }

        return false;
    }
}
