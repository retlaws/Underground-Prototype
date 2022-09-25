using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAgentNav : MonoBehaviour
{
    [SerializeField] GameObject target;

    NavMeshAgent agent; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        MoveToTarget();   
    }

    private void MoveToTarget()
    {
        agent.destination = target.transform.position;
    }
}
