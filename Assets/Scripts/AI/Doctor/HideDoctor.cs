using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HideDoctor : State
{
    public HideDoctor(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.HIDE;
    }

    public override void Enter()
    {
        base.Enter();
        agent.speed = 10f;
    }

    public override void Update()
    {
        base.Update();
        //initialise variables to remember the hiding spot that is closest to the agent.
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        //look through all potential hiding spots
        for (int i = 0; i < LevelManager.Instance.HidingPoints.Count; i++)
        {
            //determine the direction of the hiding spot from the target
            Vector3 hideDir = LevelManager.Instance.HidingPoints[i].transform.position - player.transform.position;

            //add this direction to the position of the hiding spot to find a location on the
            //opposite side of the hiding spot to where the target is
            Vector3 hidePos = LevelManager.Instance.HidingPoints[i].transform.position + hideDir.normalized * 10;

            //if this hiding spot is closer to the agent than the distance to the last one
            if (Vector3.Distance(npc.transform.position, hidePos) < dist)
            {
                //remember it
                chosenSpot = hidePos;
                dist = Vector3.Distance(npc.transform.position, hidePos);
            }
        }

        //go to the hiding location
        Seek(chosenSpot);
        if (agent.remainingDistance < 1)
        {
            nextState = new PatrolDoctor(npc, agent, anim, player, audioSource);
            stage = EVENT.EXIT;
            return;
        }
    }
}
