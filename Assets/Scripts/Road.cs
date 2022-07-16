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

    public GameObject busStop;
    public SpawnPlayer spawnPlayer;

    public static bool playerSpawned = false;

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
        if(!IsGameStart && time > 5)
        {
            time = 0;
            transform.position = initPos;
        }
        else if (IsGameStart)
        {
            busStop.SetActive(true);
            if(speed > 0)
                speed -= Time.deltaTime;
        }

        if (!playerSpawned && speed < 0.01f)
        {
            Instantiate(spawnPlayer.PlayerPrefab, spawnPlayer.SpawnPoint);
            playerSpawned = true;
        }
    }
}
