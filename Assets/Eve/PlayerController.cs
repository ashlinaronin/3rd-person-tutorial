using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveDirection;
    float jumpDirection;
    public float moveSpeed = 2;
    public float maxForwardSpeed = 8;
    public float turnSpeed = 100;
    float desiredSpeed;
    float forwardSpeed;
    float jumpSpeed = 30000f;
    bool readyJump = false;

    const float groundAcceleration = 5;
    const float groundDeceleration = 25;

    Animator animator;
    Rigidbody rigidBody;

    bool IsMoveInput
    {
        get {
            return !Mathf.Approximately(moveDirection.sqrMagnitude, 0f);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpDirection = context.ReadValue<float>();
    }

    void Move(Vector2 direction)
    {
        float turnAmount = direction.x;
        float forwardDirection = direction.y;

        // clamp direction vector to magnitude of 1
        if (direction.sqrMagnitude > 1f)
        {
            direction.Normalize();
        }        

        desiredSpeed = direction.magnitude * maxForwardSpeed * Mathf.Sign(forwardDirection);
        float acceleration = IsMoveInput ? groundAcceleration : groundDeceleration;

        forwardSpeed = Mathf.MoveTowards(forwardSpeed, desiredSpeed, acceleration * Time.deltaTime);
        animator.SetFloat("ForwardSpeed", forwardSpeed);

        transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
    }

    void Jump(float direction)
    {
        //Debug.Log(direction);

        if (direction > 0)
        {
            animator.SetBool("ReadyJump", true);
            readyJump = true;
        }
        else if (readyJump)
        {
            animator.SetBool("Launch", true);
        }
    }

    // todo: this isn't quite working-- she's not flying in the air
    // like
    public void Launch()
    {
        rigidBody.AddForce(0, jumpSpeed, 0);
        animator.SetBool("Launch", false);
        animator.applyRootMotion = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(moveDirection);
        Jump(jumpDirection);
    }
}
