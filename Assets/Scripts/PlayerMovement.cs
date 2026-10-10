using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    public float jumpHeight = 1.2f;
    public float gravity = -9.8f;

    public bool canMove = false;
    public event Action PausePressed;

    private CharacterController controller;
    private Vector2 moveInput;
    private float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;
        verticalVelocity += gravity * Time.deltaTime;

        Vector3 move = Vector3.zero;
        if (canMove)
            move = new Vector3(moveInput.x, 0f, moveInput.y) * speed;
        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }

    public void ResetPosition()
    {
        controller.enabled = false;   // si no, el CharacterController ignora el cambio
        transform.position = new Vector3(0, 1.5f, 0);
        controller.enabled = true;
        verticalVelocity = 0f;
    }

    private void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (canMove && value.isPressed && controller.isGrounded)
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void OnPause(InputValue value)
    {
        if (value.isPressed)
            PausePressed?.Invoke();
    }
}