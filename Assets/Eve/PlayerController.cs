using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveDirection;
    public float moveSpeed = 2;
    public float maxForwardSpeed = 8;
    float desiredForwardSpeed;
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

    void Move(Vector2 direction)
    {
        // clamp direction vector to magnitude of 1
        if (direction.sqrMagnitude > 1f)
        {
            direction.Normalize();
        }

        desiredForwardSpeed = direction.magnitude * maxForwardSpeed;
        float acceleration = IsMoveInput ? groundAcceleration : groundDeceleration;

        forwardSpeed = Mathf.MoveTowards(forwardSpeed, desiredForwardSpeed, acceleration * Time.deltaTime);
        animator.SetFloat("ForwardSpeed", forwardSpeed);
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
    }
}
