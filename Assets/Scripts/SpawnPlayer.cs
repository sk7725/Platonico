using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Transform SpawnPoint;

    public CinemachineVirtualCamera vcam01;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Road.playerSpawned)
        {
            PlayerControl player = FindObjectOfType<PlayerControl>();
            vcam01.m_LookAt = player.gameObject.transform;
            vcam01.m_Follow = player.gameObject.transform;
        }
    }
}
