using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGMController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // 播放背景音乐
        GetComponent<AudioSource>().Play();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
