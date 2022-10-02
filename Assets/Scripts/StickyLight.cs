using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyLight : MonoBehaviour, Iinteractable
{
    public LayerMask collisionMask;
    public float initialSpeed = 10f;
    public Vector3 velocity;
    Rigidbody rb;
    bool stuck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Init()
    {
        Vector3 localForward = transform.InverseTransformDirection(Vector3.forward);

        velocity = transform.forward * initialSpeed; 
        rb.velocity = velocity; 
    }

 
    void Update()
    {

        if (stuck)
        {
            return;
        }

        Move();
    }

    private void Move()
    {
        Vector3 moveAmount = velocity * Time.deltaTime;

        float moveDst = moveAmount.magnitude;
        Vector3 moveDir = moveAmount / moveDst;

        Ray ray = new Ray(transform.position, moveDir);
        RaycastHit hitInfo;
        if (Physics.SphereCast(ray, 0.05f, out hitInfo, moveDst, collisionMask))
        {
            transform.position = hitInfo.point;

            transform.position = hitInfo.point + hitInfo.normal * 0.1f;
            transform.forward = hitInfo.normal;

            stuck = true;
            velocity = Vector3.zero;
            rb.useGravity = false;
            rb.velocity = velocity;
            rb.isKinematic = true;
        }
        else
        {
            transform.position += moveAmount;
        }
    }

    public void interact(PlayerInteract player)
    {
        player.GetComponent<LightController>().PickupThrowableLight();
        player.MakeCurrentObjectNull();
        Destroy(gameObject); 
    }
}
