using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneImgController : MonoBehaviour {

    public Animation EnterSceneAnimation;
    public Animator animator;

	// Use this for initialization
	void Start () {
 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnterGameScene()
    {
        SceneManager.LoadScene("Game");
        FlushEventCallback();
    }

    public void EnterRankScene()
    {
        SceneManager.LoadScene("rank");
        FlushEventCallback();
    }

    public void EnterStartScene()
    {
        SceneManager.LoadScene("startgame");
        FlushEventCallback();
    }

    public void FlushEventCallback()
    {
        //清空动画帧事件，否则在下一场景无法继续跳转
        EnterSceneAnimation.clip.events = null;
        
    }
}
