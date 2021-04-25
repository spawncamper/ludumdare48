using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform target;

    void LateUpdate()
    {
        transform.position = target.position;
    }
}
