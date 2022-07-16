using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{
    Rigidbody rigidbody;
    float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector3(speed, 0, 0);

        if (Road.playerSpawned)
        {
            if(speed < 200)
            {
                speed -= 20 * Time.deltaTime;
            }
        }
    }
}
