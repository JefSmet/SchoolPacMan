using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CleverPursueCop : State
{
    public CleverPursueCop(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.CLEVERPURSUE;
    }

    public override void Enter()
    {
        base.Enter();
        agent.speed = 5;
        agent.isStopped = false;
    }

    public override void Update()
    {
        base.Update();

        if (CanAttackPlayer())
        {
            nextState = new AttackCop(npc, agent, anim, player, audioSource);
            stage = EVENT.EXIT;
            return;
        }
        else if (!CanSeePlayer())
        {
            nextState = new PatrolCop(npc, agent, anim, player, audioSource);
            stage = EVENT.EXIT;
            return;
        }
        //the vector from the agent to the target
        Vector3 targetDir = player.position - npc.transform.position;

        //the angle between the forward direction of the agent and the forward direction of the target
        float relativeHeading = Vector3.Angle(npc.transform.forward, npc.transform.TransformVector(player.forward));

        //the angle between the forward direction of the agent and the position of the target
        float toTarget = Vector3.Angle(npc.transform.forward, npc.transform.TransformVector(targetDir));

        //if the agent behind and heading in the same direction or the target has stopped then just seek.
        if ((toTarget > 90 && relativeHeading < 20) || LevelManager.Instance.PlayerMoveSpeed < 0.01f)
        {
            Seek(player.position);
            return;
        }

        //calculate how far to look ahead and add this to the seek location.
        float lookAhead = targetDir.magnitude / (agent.speed + LevelManager.Instance.PlayerMoveSpeed);
        Seek(player.position + player.forward * lookAhead);
    }
}
