using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankBtnController : MonoBehaviour {
    public Animation ChangeSceneAnimation;
    public GameObject ChangeSceneImg;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        AnimationEvent animationEvent = new AnimationEvent
        {
            functionName = "EnterRankScene",
            time = 1.5f
        };
        ChangeSceneAnimation.clip.AddEvent(animationEvent);
        ChangeSceneImg.SetActive(true);
        ChangeSceneAnimation.Play();
    }

}
