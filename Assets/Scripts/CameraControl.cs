using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public GameObject player, boss;
    public float playerOffset = 3f, speed = 2f;
    public Vector3 staticOffset = Vector3.up;
    private Vector3 targetPos;

    void Start() {
        player = GameObject.FindWithTag("Player");
        boss = GameObject.FindWithTag("Boss");
    }

    void Update() {
        Vector3 look = boss.transform.position - player.transform.position;
        look.Normalize();
        look *= playerOffset;

        transform.position = player.transform.position - look + staticOffset;
        //transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
    }
}
