using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubController : MonoBehaviour
{
    //public float speed_h = 1f;
    //public float speed_v = 1f;
    //public float speed_r = 1f;

    public float max_speed_h = 5f;
    public float max_speed_h_reverse = -2f;
    public float speed_h_acceleration = 0.5f;
    public float speed_h_delay = 0.5f;

    public float max_speed_r = 0.8f;
    public float speed_r_acceleration = 0.1f;
    public float speed_r_delay = 0.5f;

    public float max_speed_v = 2f;
    public float sinking_speed_v = 0.5f;
    public float speed_v_acceleration = 0.1f;
    public float speed_v_delay = 0.5f;

    public GameObject engine_h;
    public GameObject engine_v;
    SubEmgine eng_h, eng_v;
    public GameObject rudder;
    rudderController rud_c;

    private float current_speed_h, current_speed_r, current_speed_v;
    int turning_side;
    bool engine_h_change = false;
    bool engine_r_change = false;
    bool engine_v_change = false;
    bool engine_r_stop = false;
    bool engine_v_stop = false;

    private IEnumerator coroutine_h, coroutine_r, coroutine_v;
    // Start is called before the first frame update
    void Start()
    {
        turning_side = 1;
        current_speed_h = 0f;
        current_speed_r = 0f;
        current_speed_v = 0f;
        eng_h = engine_h.GetComponent<SubEmgine>();
        eng_v = engine_v.GetComponent<SubEmgine>();
        rud_c = rudder.GetComponent<rudderController>();
   
    }
    //***************************************************
    // ********* HORIZONTAL ENGINE CONTROL **************
    //***************************************************
    public void speed_h_up()
    {
        if (current_speed_h < max_speed_h && !engine_h_change)
        {
            engine_h_change = true;
            coroutine_h = speed_h_change(1);
            StartCoroutine(coroutine_h);
            //StartCoroutine("speed_h_change(1)");
        }
    }

    public void speed_h_down()
    {
        if (current_speed_h > max_speed_h_reverse && !engine_h_change)
        {
            engine_h_change = true;
            coroutine_h = speed_h_change(-1);
            StartCoroutine(coroutine_h);
            //StartCoroutine("speed_h_change(1)");
        }
    }


    IEnumerator speed_h_change(int gear) //gear 1 - forward, -1 back
    {
        // Debug.Log("START speed_cor speed = " + current_speed_h.ToString("F3") + "   gear = " + gear.ToString("F0"));

        float h_delay = speed_h_delay;
        float to_h_speed = current_speed_h + speed_h_acceleration * gear;
        if (to_h_speed > max_speed_h) { to_h_speed = max_speed_h; }
        if (to_h_speed < max_speed_h_reverse) { to_h_speed = max_speed_h_reverse; }

        //Debug.Log("accelerating to = " + to_h_speed.ToString("F3"));

        while ((current_speed_h + Mathf.Abs(max_speed_h_reverse) < to_h_speed + Mathf.Abs(max_speed_h_reverse) && gear == 1)
            || (current_speed_h + Mathf.Abs(max_speed_h_reverse) > to_h_speed + Mathf.Abs(max_speed_h_reverse) && gear == -1)
            )
        {
            yield return null;
            if (h_delay <= 0)
            {
                current_speed_h = current_speed_h + (speed_h_acceleration * Time.deltaTime * gear);
            }
            else
            {
                h_delay = h_delay - Time.deltaTime;
            }
            // Debug.Log("current_speed_h = " + current_speed_h.ToString("F3") + "   /  h_delay = " + h_delay.ToString("F3"));
        }
        engine_h_change = false;
        if (current_speed_h > max_speed_h) { current_speed_h = max_speed_h; }
        if (current_speed_h < max_speed_h_reverse) { current_speed_h = max_speed_h_reverse; }
        //Debug.Log("END speed_cor speed = " + current_speed_h.ToString("F3"));
    }

    //***************************************************
    // ********* RUDDER CONTROL *************************
    //***************************************************
    public void turn_left()
    {
        if (!engine_r_change)
        {
            engine_r_change = true;
            turning_side = -1;
            coroutine_r = speed_r_change(1);
            StartCoroutine(coroutine_r);
        }
    }

    public void turn_right()
    {
        if (!engine_r_change)
        {
            engine_r_change = true;
            turning_side = 1;
            coroutine_r = speed_r_change(1);
            StartCoroutine(coroutine_r);
        }
    }

    public void stop_turning_acceleration()
    {
        if(engine_r_change)
        {
            engine_r_stop = true;
        }
    }


    public void stop_turning_deccelerate()
    {
        if (!engine_r_change)
        {
            engine_r_change = true;
            //turning_side = 1;
            coroutine_r = speed_r_change(-1);
            StartCoroutine(coroutine_r);
        }
    }


    IEnumerator speed_r_change(int gear) // gear 1 - turn, -1 stop
    {
        //Debug.Log("START speed_r_change rpeed_r = " + current_speed_r.ToString("F3") + "   gear = " + gear.ToString("F0"));
        float r_delay = speed_r_delay;
        while (
                (current_speed_r < max_speed_r && gear == 1 && !engine_r_stop)
                || (current_speed_r > 0f && gear == -1 && !engine_r_stop)
              )
        {
            yield return null;
            if (r_delay <= 0)
            {
                current_speed_r = current_speed_r + (speed_r_acceleration * Time.deltaTime * gear);
            }
            else
            {
                r_delay = r_delay - Time.deltaTime;
            }
            // Debug.Log("current_speed_r = " + current_speed_r.ToString("F3"));
        }
        engine_r_stop = false;
        engine_r_change = false;
        if (current_speed_r > max_speed_r) { current_speed_r = max_speed_r; }
        if (current_speed_r < 0) { current_speed_r = 0; }
        //Debug.Log("END speed_r_change speed = " + current_speed_r.ToString("F3"));
    }

    //***************************************************
    // ********* VERTICAL ENGINE CONTROL ****************
    //***************************************************
    public void speed_v_up()
    {
        if (!engine_v_change)
        {
            engine_v_change = true;
            coroutine_v = speed_v_change(1);
            StartCoroutine(coroutine_v);
        }
    }

    public void stop_v_up()
    {
        if(engine_v_change)
        {
            engine_v_stop = true;
        }
    }

    public void speed_v_down()
    {
        if (!engine_v_change)
        {
            engine_v_change = true;
            //turning_side = 1;
            coroutine_v = speed_v_change(-1);
            StartCoroutine(coroutine_v);
        }
    }

    IEnumerator speed_v_change(int gear)
    {
        float v_delay = speed_v_delay;
        while (
                (current_speed_v < max_speed_v && gear == 1 && !engine_v_stop)
                || (current_speed_v > 0f && gear == -1 && !engine_r_stop)
               )
        {
            yield return null;
            if (v_delay <= 0)
            {
                current_speed_v = current_speed_v + (speed_v_acceleration * Time.deltaTime * gear);
            }
            else
            {
                v_delay = v_delay - Time.deltaTime;
            }
        }
        engine_v_stop = false;
        engine_v_change = false;
    }

    //***************************************************
    //***************************************************







    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            speed_h_up();
        }
        if (Input.GetKey(KeyCode.S))
        {
            speed_h_down();
        }
        
        if(Input.GetKey(KeyCode.A))
        {
            turn_left();
        }
        if (Input.GetKey(KeyCode.D))
        {
            turn_right();
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            //Debug.Log("KeyUp!");
            stop_turning_acceleration();
        }
        if(!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            stop_turning_deccelerate();
        }

        //SPACE
        if(Input.GetKey(KeyCode.Space))
        {
            speed_v_up();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //Debug.Log("KeyUp!");
            stop_v_up();
        }
        if (!Input.GetKey(KeyCode.Space))
        {
            speed_v_down();
        }



        eng_h.SetSpinSpeed(current_speed_h);
        eng_v.SetSpinSpeed(Mathf.Abs(current_speed_v));
        //rud_c.turn(current_speed_r * turning_side);

        gameObject.transform.Translate(0, (current_speed_v + sinking_speed_v) * Time.deltaTime, current_speed_h * Time.deltaTime);

        gameObject.transform.Rotate(new Vector3(0, current_speed_r * turning_side, 0), Space.World);


    }
}
