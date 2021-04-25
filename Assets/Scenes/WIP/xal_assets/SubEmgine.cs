using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SubEmgine : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationSpeed = Vector3.up;
    private Vector3 spin;

    public void SetSpinSpeed (InputAction.CallbackContext context)
    {
        spin = context.ReadValue<Vector2>().magnitude * rotationSpeed;
    }

    void Update()
    {
        transform.Rotate(spin * Time.deltaTime);
    }
}
