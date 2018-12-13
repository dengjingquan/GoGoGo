using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CemeraController : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //  每帧调用，并且在Update执行后调用
    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
