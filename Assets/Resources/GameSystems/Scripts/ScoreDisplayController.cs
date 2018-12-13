using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*********************************
 @Description   :   分数展示控制器
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.12.2
********************************/

public class ScoreDisplayController : MonoBehaviour {

    public GameObject player;
    public Text scoreText; 

	// Update is called once per frame
	void Update () {
        // 获取分数控制器的分数
        int score = player.GetComponent<PlayerScoreController>().score;
        scoreText.text = score.ToString();
	}
}
