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

    const float groundAcceleration = 5;
    const float groundDeceleration = 25;

    Animator animator;

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
        Debug.Log(direction);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(moveDirection);
        Jump(jumpDirection);
    }
}
