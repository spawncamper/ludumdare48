using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class SubController : MonoBehaviour
{
    [Header("Parameters")]

    [SerializeField][Range(0f, 1000f)]
    private float movementSpeed = 5f;
    [SerializeField][Range(0f, 1000f)]
    private float altitudeSpeed = 5f;
    [SerializeField][Range(0f, 1000f)]
    private float rotationSpeed = 5f;
    [SerializeField]
    private Vector3 waterLiftForce;
    [SerializeField]
    private float selfRightingTorque = 1f;

    [SerializeField][Tooltip("Caps the allowed speed you can reach.")]
    private float maxSpeed = 30f;
    public Vector3 inputMoveDirection = Vector3.zero;


    [Header("References")]

    private Rigidbody rBody;


    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    #region Movement
    private void ApplyMovement()
    {
        float angle = Vector3.Angle(transform.up, Vector3.up);
        Vector3 selfRightCompensation = Vector3.zero;

        if(angle > 0.001)
        {
            var axis = Vector3.Cross(transform.up, Vector3.up);
            selfRightCompensation = axis * angle * selfRightingTorque;
        }

        rBody.AddTorque((new Vector3(0, inputMoveDirection.x * rotationSpeed, 0) * Time.fixedDeltaTime) + selfRightCompensation);

        Vector3 accelerationForce = inputMoveDirection;
        accelerationForce.x = 0f;
        accelerationForce.z *= movementSpeed;
        accelerationForce.y *= altitudeSpeed;

        rBody.AddRelativeForce((accelerationForce + waterLiftForce) * Time.fixedDeltaTime);

        if(rBody.velocity.sqrMagnitude > maxSpeed * maxSpeed)
            rBody.velocity = rBody.velocity.normalized * maxSpeed;
    }
    #endregion

    #region Input
    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        inputMoveDirection = new Vector3(input.x, inputMoveDirection.y, input.y);
    }
    public void OnAltitude(InputAction.CallbackContext context)
    {
        inputMoveDirection.y = context.ReadValue<float>();
    }
    #endregion
}
