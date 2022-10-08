using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneTarget : MonoBehaviour
{
    public Transform target; 
    NavMeshAgent agent;

    public float targetCurrentSpeed = 0;
    Vector3 previousPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(target.position);
        UpdateCurrentSpeed();
    }

    private void UpdateCurrentSpeed()
    {
        Vector3 curMove = transform.position - previousPosition;
        targetCurrentSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;
    }
}
