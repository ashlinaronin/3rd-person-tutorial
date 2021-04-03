using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveDirection;
    public float moveSpeed = 2;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    void Move(Vector2 direction)
    {
        // using direction.y to move character in z
        transform.Translate(direction.x * moveSpeed * Time.deltaTime, 0, direction.y * moveSpeed * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(moveDirection);
    }
}
