using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour {

    public GameObject rotateObject;
    public Vector3 rotateSpeed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        rotateObject.transform.Rotate(rotateSpeed);

	}
}
