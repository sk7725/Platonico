using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStop : MonoBehaviour
{
    public static bool IsGameStart;
    public float speed;
    Rigidbody rigidbody;

    void Start()
    {
        speed = 0;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameStart)
        {
            rigidbody.enabled = true;
            SetSpeed();
            if (speed > 0)
                speed -= Time.deltaTime;
        }
    }

    void SetSpeed()
    {
        speed = 10;
    }
}
