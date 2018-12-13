using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*********************************
 @Description   :   开始倒计时文本控制器
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.12.9
********************************/

public class StartTextController : MonoBehaviour {

    Vector3 initPosition;               // 初始位置
    public GameObject[] gameObjects;    // 需要激活的游戏对象
    public int num = 3;                 // 倒计数
    int currNum;                        // 当前倒计数
    float startTime;                    // 游戏开始时间
    public Camera mainCamera;           // 摄像机对象


    // Use this for initialization
    void Start () {
        startTime = Time.time;
        initPosition = this.transform.position;
        
        currNum = 0;
        //Debug.Log("init");
        
	}
	
	// Update is called once per frame
	void Update () { 
        if (num - currNum >= 0)
        {
            // 下坠效果
           
            this.transform.position = new Vector3(this.transform.position.x,
                                        this.transform.position.y - Time.deltaTime * 50,
                                        this.transform.position.z);
            if (Time.time - startTime > currNum)
            {
                GetComponent<Text>().text = (num - currNum).ToString();
                currNum += 1;
                // 返回初始位置
                this.transform.position = initPosition;
            }
            
        }
        // 倒计时结束 开启游戏
        else
        {
            GetComponent<Text>().text = "Go!";
            // 激活对象
            foreach (var gameObject in gameObjects){
                gameObject.SetActive(true);
            }
            // 激活摄像机跟随
            mainCamera.GetComponent<ThirdPersonCemeraController>().enabled = true;
            Destroy(this.gameObject,2f);
        }
	}
}
