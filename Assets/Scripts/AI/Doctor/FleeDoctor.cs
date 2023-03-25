using UnityEngine;
using UnityEngine.AI;

internal class FleeDoctor : State
{
    float stopFleeingDistance = 20.0f;

    public FleeDoctor(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.FLEE;
    }


    public override void Enter()
    {
        base.Enter();
        agent.isStopped = false;
        agent.speed = 10.0f;
    }

    public override void Update()
    {
        base.Update();
        if (Vector3.Distance(npc.transform.position, player.position) > stopFleeingDistance)
        {
            nextState = new HideDoctor(npc, agent, anim, player, audioSource);
            stage = EVENT.EXIT;
            return;
        }
        Flee(player.position);
    }
}