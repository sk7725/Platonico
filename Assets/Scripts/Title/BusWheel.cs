using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusWheel : MonoBehaviour
{
    public static bool IsGameStart;
    public float Speed = 200;
    public bool IsSpinning;

    // Update is called once per frame
    void Update()
    {
        if(IsSpinning)
            transform.Rotate(Speed * Time.deltaTime, 0, 0);

        if (IsGameStart)
            Speed -= 20 * Time.deltaTime;
    }
}
