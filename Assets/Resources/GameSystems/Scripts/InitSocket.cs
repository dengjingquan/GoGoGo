using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSocket : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GameSocket.client == null)
        {
            GameSocket.InitTCP();
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
