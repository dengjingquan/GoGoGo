using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;

    public float Speed;

    private int Collect;

    public Text CollectText;

    public Text WinText;

    public Text TimeText;

    public float StartTime;

    bool IsStart;

    bool IsEnd;

    public Text StartText;

    public string FilePath;

    public Text UserNameText;
  
    public void Start()
    {       
        //获取属性
        rb = GetComponent<Rigidbody>();
        Collect = 0;
        IsStart = false;
        IsEnd = false;
    }


    //渲染每帧前执行
    private void Update()
    {
        if(!IsStart)
        {
            //按空格开始游戏
            bool SpaceUp = Input.GetKeyUp(KeyCode.Space);
            if (SpaceUp)
            {
                IsStart = true;
                StartText.gameObject.SetActive(false);
                StartTime = Time.time;
                //Debug.Log(StartTime);
            }
        }
        else
        {
            //刷新时间
            if (!IsEnd)
            {
                //Debug.Log(Time.time - StartTime);
                TimeText.text = "Time: "+(Time.time - StartTime).ToString("0.00") + " s";
            }

        }
    }

    //执行物理计算前才调用，用于实现物理计算
    private void FixedUpdate()
    {

        if (IsStart)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            // 力的方向 三维 y为0 只做水平移动
            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
            rb.AddForce(movement*Speed);
        }
        
    }

    //首次碰撞
    private void OnTriggerEnter(Collider other)
    {
        //判断对象 字符串法
        //if (gameObject.tag == "Some...")
        //{

        //}

        //内置方法 更高效
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            Collect ++;
            SetContText();
        }
        //碰撞销毁
        //Destroy(other.gameObject);

        //禁用
        //gameObject.SetActive()
        if (Collect >=12)
        {
            //游戏结束 记录成绩
            IsEnd = true;
            ShowWinContext();
            string ScoreRecord = "\r\n"
                + UserNameText.text.ToString() 
                + " "
                + (Time.time - StartTime).ToString("0.00");
            File.AppendAllText(FilePath,ScoreRecord);
        }

    }

    void SetContText()
    {
        CollectText.text = "Collect : " + Collect.ToString();
    }

    void ShowWinContext()
    {
        WinText.gameObject.SetActive(true);
    }

}


