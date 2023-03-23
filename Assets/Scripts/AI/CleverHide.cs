using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CleverHide : State
{
    //look for a hiding spot but determine where the agent must stand
    //based on the boundary of the object determined by a box collider
    public CleverHide(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, AudioSource audioSource) : base(npc, agent, anim, player, audioSource)
    {
        name = STATE.CLEVERHIDE;
    }

    public override void Enter()
    {
        base.Enter();
        agent.speed = 3f;
        if (anim != null)
        {
            anim.SetTrigger("isWalking");
        }
    }

    public override void Update()
    {
        base.Update();
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;
        GameObject chosenGO = LevelManager.Instance.HidingPoints[0];

        //same logic as for Hide() to find the closest hiding spot
        for (int i = 0; i < LevelManager.Instance.HidingPoints.Count; i++)
        {
            Vector3 hideDir = LevelManager.Instance.HidingPoints[i].transform.position - player.position;
            Vector3 hidePos = LevelManager.Instance.HidingPoints[i].transform.position + hideDir.normalized * 100;

            if (Vector3.Distance(npc.transform.position, hidePos) < dist)
            {
                chosenSpot = hidePos;
                chosenDir = hideDir;
                chosenGO = LevelManager.Instance.HidingPoints[i];
                dist = Vector3.Distance(npc.transform.position, hidePos);
            }
        }

        //get the collider of the chosen hiding spot
        Collider hideCol = chosenGO.GetComponent<Collider>();
        //calculate a ray to hit the hiding spot's collider from the opposite side to where
        //the target is located
        Ray backRay = new Ray(chosenSpot, -chosenDir.normalized);
        RaycastHit info;
        float distance = 250.0f;
        //perform a raycast to find the point near the array
        hideCol.Raycast(backRay, out info, distance);

        //go and stand at the back of the object at the ray hit point
        Seek(info.point + chosenDir.normalized);
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
