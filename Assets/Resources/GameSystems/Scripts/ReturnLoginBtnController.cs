using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnLoginBtnController : MonoBehaviour {

    public GameObject loginUI;
    public GameObject startGameUI;
    public GameObject enterSceneImg;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        // 注销
        GameSocket.isOnline = false;
        SceneManager.LoadScene("startgame");
    }
}
