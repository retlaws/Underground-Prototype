using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class DroneGhostMovement : MonoBehaviour
{
    [SerializeField] LayerMask terrainLayerMask;
    [SerializeField] Transform droneGhostTarget; 

    float widthOfDrone = 1f; 

    float distanceRight = 0; 
    float distanceLeft = 0;
    float distanceUpwards = 0;
    float distanceDownwards = 0;



    private void Start()
    {
        widthOfDrone = GetComponent<SphereCollider>().radius; //TODO this only works if the drone is spherical and we use just a sphere collider 
    }

    // Update is called once per frame
    void Update()
    {
        //MeasureWidthOfOpening();
        //MeasureDistanceForward();
        MeasureHeightOfOpening();
        RotateToTarget();
        MoveToTarget();
    }

    //private void MeasureDistanceForward()
    //{
    //    RaycastHit forwardRaycastHit; 

    //    Physics.SphereCast(transform.position, widthOfDrone, transform.forward, out forwardRaycastHit, Mathf.Infinity, terrainLayerMask);
    //    Debug.DrawLine(transform.position, forwardRaycastHit.point, Color.blue);
    //}

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

    private void RotateToTarget() // TODO need to change this so that it lerps rather than turns immediately
    {
        Vector3 droneTargetYOnly = new Vector3(droneGhostTarget.position.x, transform.position.y, droneGhostTarget.position.z); // this means that the drone flies level rather than directly aiming at the drone target

        Vector3 relativePos = droneTargetYOnly - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }

    private void MoveToTarget()
    {
        float ghostTargetSpeed = droneGhostTarget.GetComponent<NavMeshAgent>().speed;

        float heightDifference = distanceUpwards - ((distanceUpwards + distanceDownwards) / 2);


        Vector3 targetLocation = new Vector3(0, heightDifference + heightDifference, 1);
        
        transform.Translate(targetLocation * ghostTargetSpeed * Time.deltaTime, Space.Self);
    }

}
