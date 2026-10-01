using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput PlayerInputMap;
    public float speed = 3f;
    public float jump = 3f;

    private Vector3 inputDirecction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnMuveInput(PlayerInputMap);
    }

    private void OnMuveInput(InputValue value)
    {
        Vector2 inputValue = value.Get<Vector2>();
        inputDirecction = new Vector3 (inputValue.x, 0, inputValue.y);
    }
}
