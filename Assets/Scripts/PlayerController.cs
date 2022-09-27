using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Header("Movement variables")]
    public float maxSpeed = 10f;
    [SerializeField] float acceleration = 2f;
    [SerializeField] float deceleration = 0.5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float jumpDecelerationAmount = 3f;
    bool isGrounded = true;

    [Header("Rotation variables")]
    [SerializeField] GameObject followTransform;
    [SerializeField] float rotationPower = 3f;
    [SerializeField] int lowestHeadAngle = 25;
    [SerializeField] int highestHeadAngle = 150;

    Vector2 lookVector;
    Vector2 rawMovementInput;
    public Vector3 movementVector = new Vector3(0, 0, 0);
    float distToGround; 

    PlayerInput playerInput;
    PlayerDig playerDig;
    Rigidbody rb;

    InputAction move;
    InputAction look;
    InputAction fire;
    InputAction fireHold;
    InputAction jump;
    InputAction interact; 


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerDig = GetComponent<PlayerDig>();
        rb = GetComponent<Rigidbody>();

        GetInputActions();
    }

    private void GetInputActions()
    {
        move = playerInput.actions["Move"];
        look = playerInput.actions["Look"];
        fire = playerInput.actions["Fire"];
        fireHold = playerInput.actions["FireHold"];
        jump = playerInput.actions["Jump"];
        interact = playerInput.actions["Interact"];
    }

    private void OnEnable()
    {
        fire.started += playerDig.OnFire;
        fireHold.started += playerDig.OnFireHoldStart;
        fireHold.performed += playerDig.OnFireHoldPerformed;
        jump.performed += Jump;
        interact.performed += Interact;
    }

    private void OnDisable()
    {
        fire.started -= playerDig.OnFire;
        fireHold.started -= playerDig.OnFireHoldStart;
        fireHold.performed -= playerDig.OnFireHoldPerformed;
        jump.performed -= Jump;
        interact.performed -= Interact;
    }


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void Update()
    {
        HeadRotation();
        rawMovementInput = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        IsGrounded();
        UpdateDeceleration(); // could optimise this as don't need to run this if accelerating and vice versa
        UpdateAcceleration();
        rb.velocity = transform.TransformDirection(movementVector);
    }

    private void IsGrounded()
    {
        isGrounded =  Physics.SphereCast(transform.position, 0.5f, -Vector3.up, out RaycastHit hit, distToGround - 0.4f);
    }

    private void UpdateDeceleration()
    {
        float jumpDecelerationMultiplier = jumpDecelerationAmount;

        if (isGrounded)
        {
            jumpDecelerationMultiplier = 1f;
        }


        movementVector.y = rb.velocity.y;

        if (!isGrounded || rawMovementInput.x == 0 && movementVector.x > 0)
        {
            movementVector.x = Mathf.Clamp(movementVector.x - (deceleration * Time.deltaTime * jumpDecelerationMultiplier), 0, maxSpeed);
        }
        else if (!isGrounded || rawMovementInput.x == 0 && movementVector.x < 0)
        {
            movementVector.x = Mathf.Clamp(movementVector.x + (deceleration * Time.deltaTime * jumpDecelerationMultiplier), maxSpeed * -1, 0);
        }

        if (!isGrounded || rawMovementInput.y == 0 && movementVector.z > 0)
        {
            movementVector.z = Mathf.Clamp(movementVector.z - (deceleration * Time.deltaTime * jumpDecelerationMultiplier), 0, maxSpeed);
        }
        else if (!isGrounded || rawMovementInput.y == 0 && movementVector.z < 0)
        {
            movementVector.z = Mathf.Clamp(movementVector.z + (deceleration * Time.deltaTime * jumpDecelerationMultiplier), maxSpeed * -1, 0);
        }
    }

    private void UpdateAcceleration()
    {
        if (!isGrounded) { return; }
        if (rawMovementInput.x > 0 && movementVector.x < maxSpeed)
        {
            movementVector.x += rawMovementInput.x * Time.deltaTime * acceleration;
        }
        if (rawMovementInput.x < 0 && movementVector.x > maxSpeed * -1)
        {
            movementVector.x += rawMovementInput.x * Time.deltaTime * acceleration;
        }
        if (movementVector.z < maxSpeed)
        {
            movementVector.z += rawMovementInput.y * Time.deltaTime * acceleration;
        }
    }

    private void HeadRotation()
    {
        lookVector = look.ReadValue<Vector2>();
        followTransform.transform.rotation *= Quaternion.AngleAxis(lookVector.x * rotationPower, Vector3.up);
        followTransform.transform.rotation *= Quaternion.AngleAxis(lookVector.y * -1 * rotationPower, Vector3.right);

        //Clamp the Up/Down rotation
        Vector3 angles = followTransform.transform.localEulerAngles;
        angles.z = 0;
        float angle = followTransform.transform.localEulerAngles.x;
        
        if (angle > 180 && angle < 320)
        {
            angles.x = 320;
        }
        else if (angle < 180 && angle > 90 - lowestHeadAngle)
        {
            angles.x = 90 - lowestHeadAngle;
        }
        followTransform.transform.localEulerAngles = angles;

        //this dictates the y axis of the player body // 
        transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);

        //this dictates the x axis of the player "head"
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!isGrounded) { return; }    
        rb.AddForce(jumpForce * transform.up, ForceMode.Impulse);
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if(currentObject != null)
        {
            currentObject.interact();
        }
    }

    Iinteractable currentObject = null; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<Iinteractable>() != null)
        {
            currentObject = other.transform.GetComponent<Iinteractable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentObject = null;
    }

}