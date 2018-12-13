using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitImageController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(transform.forward, Time.deltaTime * 90, Space.Self);
	}
}
