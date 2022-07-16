using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public bool IsGameStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameStart)
        {
            BusStop.IsGameStart = true;
            Road.IsGameStart = true;
            BusWheel.IsGameStart = true;
        }
    }
}
