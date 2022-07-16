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
        gameObject.SetActive(false);
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector3(speed, 0, 0);

        if (IsGameStart)
        {
            gameObject.SetActive(true);
            if (speed > 0)
                speed -= Time.deltaTime;
        }
    }
}
