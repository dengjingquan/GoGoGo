using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void go()
    {
        Debug.Log("play");
        GetComponent<Animation>().Play("New Animation");
        Debug.Log("played");
    }
}
