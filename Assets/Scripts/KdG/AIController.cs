using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //target= GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(target.transform.position);
        //if (agent.remainingDistance < 3)
        //{
        //    agent.isStopped= true;
        //}
        //else
        //{
        //    agent.isStopped= false;            
        //}
    }
}
