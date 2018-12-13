using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*********************************
 @Description   :   游戏结束控制器
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.12.9
********************************/


public class GameOverController : MonoBehaviour {

    public Text timeText;               // 时间对象
    public Text resultText;             // 
    Collider bodyCollider;              // 身体碰撞器
    public Button againButton;          // 再次游戏按钮


    // Use this for initialization
    void Start () {
        bodyCollider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   

   
    public void GameOver(bool isWin)
    {
        // 停止记时
        timeText.GetComponent<TimeTextValueController>().isGameOver = true;
        bodyCollider.enabled = false;
        GetComponent<PlayerMoveController>().enabled = false;
        // GetComponent<Tr>
        if (isWin)
        {
            // 获取时间
            //Debug.Log( "end :" + timeText.text);
            // 发送记录到数据库
            float useTime = timeText.GetComponent<TimeTextValueController>().useTime;

            Dictionary<string, object> scoreDict = new Dictionary<string, object>();
            string action = "recordscore";
            scoreDict.Add("action", action);
            scoreDict.Add("account", GameSocket.account);
            scoreDict.Add("password", GameSocket.password);
            scoreDict.Add("score", useTime);
            //Debug.Log("====");
            //Debug.Log(GameSocket.account);
            //Debug.Log(GameSocket.password);

            // 动作ID 用于后续异步获取结果,组成 : 主机名 + 时间戳 + 账号名  + action 此处默认服务端接收 不作客户端等待回复处理
            string jsonData = JsonConvert.SerializeObject(scoreDict);
            // 发送成绩
            GameSocket.Send(jsonData);
            resultText.text = "Y O U   W I N";
        }
        else
        {
            resultText.text = "Y O U  F A I L";
        }
        againButton.gameObject.SetActive(true);
    }
}
