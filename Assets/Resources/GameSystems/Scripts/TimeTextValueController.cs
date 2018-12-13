using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*********************************
 @Description   :   敌人攻击控制器
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.12.8
********************************/
public class TimeTextValueController : MonoBehaviour {

    public int Scoend;         // 规定用时
    float startTime;           // 开始时间
    float maxTime;             // 截止时间
    public float useTime;      // 用时成绩
    public bool isGameOver;    // 游戏结束
    public GameObject player;  // 玩家对象
   


	// Use this for initialization
	void Start () {
        maxTime = Scoend + Time.time;
        isGameOver = false;
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {      
        if (!isGameOver)
        {
            float time = maxTime - Time.time;
            useTime = Time.time - startTime;
            if (time > 0)
            {
                
                string[] timeFormat = TimeFormat(time);
                this.GetComponent<Text>().text = timeFormat[0] + ":" + timeFormat[1] + " s";
            }
            else
            {
                isGameOver = true;
                player.GetComponent<GameOverController>().GameOver(false);
            }
        }
	}

    // 转换为 分 : 秒
    string[] TimeFormat(float seconds)
    {
        int minute = (int)(seconds / 60);
        int second = (int)(seconds % 60);
        string[] time = new string[] { minute.ToString(),second.ToString() };
        return time;
    }
}
