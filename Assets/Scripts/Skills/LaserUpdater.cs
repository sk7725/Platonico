using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserUpdater : MonoBehaviour{
    public float dps = 50f;

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            PlayerControl.health -= dps * Time.deltaTime;
            Debug.Log("P" + PlayerControl.health);
        }
    }
}
