using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class DroneMovement : MonoBehaviour
{
    [SerializeField] LayerMask terrainLayerMask;
    [SerializeField] Transform droneTargetTransform;
    [SerializeField] float lookAtFinalObjectiveSpeed = 1f;
    [SerializeField] float rotateToDroneTargetSpeed = 0.1f;
    [SerializeField] float targetTetherLength = 5f; 

    float widthOfDrone = 1f; 
    float distanceUpwards = 0;
    float distanceDownwards = 0;
    float droneTargetSpeed = 0;
    float droneMaxSpeed = 0;
    DroneTarget droneTarget;



    private void Start()
    {
        widthOfDrone = GetComponent<SphereCollider>().radius; //TODO this only works if uses a sphere collider 
        droneTarget = droneTargetTransform.GetComponent<DroneTarget>();
        droneMaxSpeed = droneTargetTransform.GetComponent<NavMeshAgent>().speed;
    }

    float timeDelay = 0;

    void Update()
    {
        MeasureDistanceForward();
        MeasureHeightOfOpening();
        MoveAndRotate();
    }

    private void MoveAndRotate()
    {
        droneTargetSpeed = droneTarget.targetCurrentSpeed;
        if (droneTargetSpeed > 0)
        {
            timeDelay = 0;
            RotateToTarget();
            MoveToTarget();
        }
        else
        {
            timeDelay += Time.deltaTime;
        }

        if (timeDelay > 1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, CalculateAngleToFinalObjective(), lookAtFinalObjectiveSpeed * Time.deltaTime);
        }
    }

    private Quaternion CalculateAngleToFinalObjective()
    {
        Quaternion rotation = new Quaternion();

        if (droneTarget.target != null)
        {
            Vector3 relativePos = droneTarget.target.transform.position - transform.position;
            rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        }
        return rotation;
    }

    private void MeasureDistanceForward()
    {
        RaycastHit forwardRaycastHit;

        Physics.SphereCast(transform.position, widthOfDrone, transform.forward, out forwardRaycastHit, Mathf.Infinity, terrainLayerMask);
        Debug.DrawLine(transform.position, forwardRaycastHit.point, Color.blue);
    }

    //private void MeasureWidthOfOpening()
    //{
    //    RaycastHit rightRayCastHit;
    //    RaycastHit leftRayCastHit;

    //    Physics.SphereCast(transform.position, widthOfDrone, transform.right, out rightRayCastHit, Mathf.Infinity, terrainLayerMask);
    //    Physics.SphereCast(transform.position, widthOfDrone, transform.right * -1, out leftRayCastHit, Mathf.Infinity, terrainLayerMask);
    //    Debug.DrawLine(leftRayCastHit.point, rightRayCastHit.point, Color.red);

    //    distanceRight = rightRayCastHit.distance; 
    //    distanceLeft = leftRayCastHit.distance;
    //}

    private void MeasureHeightOfOpening()
    {
        RaycastHit UpwardRaycastHit;
        RaycastHit DownwardRaycastHit;

        Physics.SphereCast(transform.position, widthOfDrone, transform.up, out UpwardRaycastHit, Mathf.Infinity, terrainLayerMask);
        Physics.SphereCast(transform.position, widthOfDrone, Vector3.down, out DownwardRaycastHit, Mathf.Infinity, terrainLayerMask);
        Debug.DrawLine(UpwardRaycastHit.point, DownwardRaycastHit.point, Color.green);

        distanceUpwards = UpwardRaycastHit.distance;
        distanceDownwards = DownwardRaycastHit.distance;
    }

    private void RotateToTarget() 
    {
        Vector3 droneTargetYOnly = new Vector3(droneTargetTransform.position.x, transform.position.y, droneTargetTransform.position.z); // this means that the drone flies level rather than directly aiming at the drone target

        Vector3 relativePos = droneTargetYOnly - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lookAtFinalObjectiveSpeed * Time.deltaTime);
    }

    private void MoveToTarget()
    {
        float heightDifference = distanceUpwards - ((distanceUpwards + distanceDownwards) / 2);

        Vector3 targetLocation = new Vector3(0, heightDifference, 1);

        float speed = droneMaxSpeed;
        float distanceToTarget = Vector3.Distance(transform.position, droneTargetTransform.position);

        if (distanceToTarget < targetTetherLength)
        {
            speed = droneMaxSpeed * (distanceToTarget / targetTetherLength);
        }
        
        transform.Translate(targetLocation * speed * Time.deltaTime, Space.Self);
    }

}
