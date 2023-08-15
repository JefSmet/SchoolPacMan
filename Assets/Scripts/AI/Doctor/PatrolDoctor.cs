using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolDoctor : State
{
    private int currentIndex = 0;
    private float visDist = 30.0f;
    private float visAngle = 90f;

    public PatrolDoctor(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.PATROL;
    }

    public override void Enter()
    {
        base.Enter();
        agent.speed = 1.5f;
        agent.isStopped = false;
    }

    public override void Update()
    {
        base.Update();        
        if (PlayerCanSeeNpc()&&DocCanSeePlayer())
        {
            nextState = new FleeDoctor(npc, agent, anim, player, audioSource);
            stage = EVENT.EXIT;
        }
        else if (DocCanSeePlayer())
        {
            nextState = new PursueDoctor(npc, agent, anim, player, audioSource);
            stage = EVENT.EXIT;
            return;
        }
        else if (agent.remainingDistance < 1 && !agent.pathPending)
        {
            currentIndex = Random.Range(0, LevelManager.Instance.PatrolPoints.Count - 1);
            agent.SetDestination(LevelManager.Instance.PatrolPoints[currentIndex].transform.position);
        }
    }

    public bool DocCanSeePlayer()
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
}
