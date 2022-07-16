using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public static bool IsGameStart;

    Vector3 initPos;
    Rigidbody rigidbody;
    public float speed;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector3(speed, 0, 0);

        time += Time.deltaTime;
        if(time > 5)
        {
            time = 0;
            transform.position = initPos;
        }
    }
}
