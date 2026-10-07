using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Rigidbody rb;
    public float speed = 3f;
    public Vector3 jumpForce = new Vector3(0, 3f, 0f);

    private Vector3 inputDirecction;

    private void Awake()
    {
        //moveAction += OnMuvement;

        
    }    

    private void OnEnable()
    {
        //moveAction.Enable();
    }

    private void OnDisable()
    {
        //moveAction.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = (this.transform.forward * inputDirecction.y * inputDirecction.x);
        controller.Move(inputDirecction.normalized *  speed * Time.deltaTime);
    }

    private void OnMuvement(InputValue value)
    {
        Vector2 inputValue = value.Get<Vector2>();
        inputDirecction = new Vector3 (inputValue.x, 0, inputValue.y);
    }

    private void OnLook(InputValue value)
    {

    }

    private void OnJump(InputValue value)
    {
        Vector3 inputValue = value.Get<Vector3>();
        rb.AddForce(jumpForce, ForceMode.Impulse);
    }
}
