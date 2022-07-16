using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Transform SpawnPoint;

    public CinemachineVirtualCamera vcam01;
    bool camIsReset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Road.playerSpawned && !camIsReset)
        {
            PlayerControl player = FindObjectOfType<PlayerControl>();
            vcam01.m_LookAt = player.gameObject.transform;
            vcam01.m_Follow = player.gameObject.transform;
            StartCoroutine(WaitAndReset());
        }
    }

    IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(1f);
        vcam01.m_LookAt = null;
        vcam01.m_Follow = null;
        camIsReset = true;
    }
}
