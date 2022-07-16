using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToScene : MonoBehaviour {
    public string scene;
    public bool needShift = true;

    private void OnTriggerStay(Collider other) {
        if (!other.CompareTag("Player")) return;
        if(!needShift || Input.GetKey(KeyCode.LeftShift)) {
            SceneLoader.LoadScene(scene);
        }
    }
}
