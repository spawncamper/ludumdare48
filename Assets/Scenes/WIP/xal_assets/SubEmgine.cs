using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubEmgine : MonoBehaviour
{
    // Start is called before the first frame update
    private float spin = 0f;
    public float rotate_coeff = 2f;
    private Vector3 rotationVector;
    void Start()
    {
        rotationVector = new Vector3(0, 0, 0);
    }

    // Update is called once per frame

    public void SetSpinSpeed (float speed)
    {
        spin = speed * rotate_coeff;
        rotationVector = new Vector3(0, spin, 0);
    }

    void Update()
    {
        transform.Rotate(rotationVector);
    }
}
