using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubEmgine : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationSpeed = Vector3.up;
    private Vector3 spin;

    public void SetSpinSpeed (float speed)
    {
        spin = speed * rotationSpeed;
    }

    void Update()
    {
        transform.Rotate(spin * Time.deltaTime);
    }
}
