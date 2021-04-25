using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubController : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float altitudeSpeed = 5f;

    [Header("References")]
    [SerializeField]
    private SubEmgine eng_h, eng_v;
    [SerializeField]
    private rudderController rud_c;

}
