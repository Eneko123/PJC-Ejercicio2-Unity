using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    public float jumpHeight = 1.2f;
    public float gravity = -9.8f;

    public GameObject thirdPersonCamera;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool onPause;
    private float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Gravedad manual
        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;
        verticalVelocity += gravity * Time.deltaTime;

        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y) * speed;
        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);

        //Vector3 look = new Vector3(0f, lookInput.y, 0f);
        //transform.rotation = Quaternion.Euler(transform.eulerAngles + look);
    }

    private void OnMovement(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void OnPause(InputValue value)
    {
        if (value.Get<bool>())
        {
            onPause = true;
        }
        else if (!value.isPressed)
        {
            onPause = false;
        }
    }

    public bool GetOnPause()
    {
        return onPause;
    }
}