using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        transform.LookAt(target);
    }
}