using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStop : MonoBehaviour
{
    public static bool IsGameStart;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (IsGameStart)
        {

            if (speed > 0)
                speed -= Time.deltaTime;
        }
    }
}
