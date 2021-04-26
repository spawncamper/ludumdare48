using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rudderController : MonoBehaviour
{
    private Vector3 turnVector;
    float max_rotation = 30f;
    Quaternion return_angle;
    Quaternion turn_angle;

    Vector3 rud_vector_right, rud_vector_left, rud_vector_forward;

    Quaternion rudder_forward;
    Quaternion rudder_right;
    Quaternion rudder_left;
    float rotation;
    float turnTime = 1f;
    bool turning = false;

    IEnumerator coruotine;

    private float timeCount;
    // Start is called before the first frame update
    void Start()
    {
        turnVector = new Vector3(0, 0, 0);

        rud_vector_forward = new Vector3(0, 0, 0);
        rud_vector_left = new Vector3(0, -30, 0);
        rud_vector_right = new Vector3(0, 30, 0);
        rudder_forward = transform.rotation;
        rudder_left = Quaternion.FromToRotation(rud_vector_forward, rud_vector_left);
        rudder_right = Quaternion.FromToRotation(transform.position, rud_vector_right);
    }
    public void turn(float degree)
    {
        turnVector.y = degree * -1; //reverse side turning
        rotation = degree * -1;

        if(!turning)
        {
            turning = true;
            if(rotation > 0)
            {
                coruotine = turning_rudder(transform.rotation, rudder_right);
            }
            if (rotation < 0)
            {
                coruotine = turning_rudder(transform.rotation, rudder_left);
            }
            if (rotation == 0)
            {
                coruotine = turning_rudder(transform.rotation, rudder_forward);
            }

            StartCoroutine(coruotine);
        }

    }



    IEnumerator turning_rudder(Quaternion from_rotation, Quaternion to_rotation)
    {
        timeCount = 0.0f;
        while (timeCount < turnTime)
        {
            yield return null;
            Debug.Log("rotation = " + rotation.ToString("F3"));

            transform.rotation = Quaternion.Slerp(from_rotation, to_rotation, timeCount);
            timeCount = timeCount + Time.deltaTime;
        }
        turning = false;
    }


    // Update is called once per frame
    void Update()
    {
        /*
        Debug.Log("rotation = " + rotation + "    transform.rotation.eulerAngles.y = " + transform.localEulerAngles.y + " turnVector = " + turnVector.ToString());
        if (rotation >= 0)
        {
            if(transform.localEulerAngles.y < max_rotation)
            {

                transform.Rotate(turnVector, Space.Self);
            }

        }
        if(rotation < 0)
        {
            if (transform.localEulerAngles.y < 360 - max_rotation)
            {
                transform.Rotate(turnVector, Space.Self);

            }

        }
        if(rotation == 0)
        {

        }
        */
    }
}
