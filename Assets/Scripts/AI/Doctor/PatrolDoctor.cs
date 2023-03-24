using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolDoctor : State
{
    int currentIndex = 0;
    float visDist = 30.0f;
    float visAngle = 90f;

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
        if (DocCanSeePlayer()&&!PlayerCanSeeNpc())
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
        else if (PlayerCanSeeNpc())
        {
            nextState = new FleeDoctor(npc, agent, anim, player, audioSource);
            stage = EVENT.EXIT;
        }
    }

    public bool DocCanSeePlayer()
    {
        Vector3 direction = player.transform.position - npc.transform.position;
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
    }
}
