using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Threading;

public class LoginBtnController : MonoBehaviour {
    public InputField accountInputField;         // 用户名输入框
    public InputField passwordInputField;        // 密码输入框
    public Text errorText;                       // 错误提示文本
    bool isLogining;                             // 是否正在登陆
    public GameObject waitImage;                 // 登陆中等待图标
    public int maxWaitTime = 10;                 // 最长登陆等待时间
    public float maxTime;
    public GameObject loginUI;                   // 登陆窗口
    public GameObject startGameUI;               // 开始游戏窗口
    public GameObject enterSceneImg;
    private string account;
    private string password;
    private string actionID;
    bool isSend;


    // Use this for initialization
    void Start () {
        isLogining = false;
        isSend = false;
	}
	
	// Update is called once per frame
	void Update () {
        // 正处于登陆状态
		if (isSend)
        {
            if (maxTime > Time.time)
            {
                if (GameSocket.responseBuffer.ContainsKey(actionID))
                {
                    // 登陆成功
                    Dictionary<string, object> resultDict = (Dictionary<string, object>)(GameSocket.responseBuffer[actionID]);
                    if (((string)(resultDict["result"])).Equals("success"))
                    {
                        GameSocket.account = account;
                        GameSocket.password = password;
                        GameSocket.isOnline = true;
                        // 还原各控件
                        errorText.text = "";
                        passwordInputField.text = "";
                        enterSceneImg.SetActive(true);
                        waitImage.SetActive(false);
                        startGameUI.SetActive(true);
                        isLogining = false;

                        loginUI.SetActive(false);                     
                    }
                    else
                    {
                        errorText.gameObject.SetActive(true);
                        errorText.text = "Error Password!";
                        waitImage.SetActive(false);
                        isLogining = false;                  
                    }
                }
            }
            else
            {
                errorText.text = "overtime try again later!";
                waitImage.SetActive(false);
                isLogining = false;
            }
        }
	}

    public void OnClick()
    {  
        // 防止按钮重复触发
        if (!isLogining)
        {
            isLogining = true;
            account = accountInputField.text;
            password = passwordInputField.text;
            // 本地验证
            if (check(account, password))
            {
                // 激活等待图标
                waitImage.SetActive(true);
                // 动作ID 用于后续异步获取结果,组成 : 主机名 + 时间戳 + 账号名 
                actionID = GameSocket.hostName + Time.time + account;
                Dictionary<string, object> loginDict = new Dictionary<string, object>
                {
                    { "action", "login" },
                    { "account", account },
                    { "password", password },
                    { "actionID", actionID}
                };
                string jsonData = JsonConvert.SerializeObject(loginDict);
                GameSocket.Send(jsonData);
                // 登陆结果答复包(通过动作ID从答复包缓冲池获取){ action:"login"   result:"success"|"fail" }
                // 验证密码
                isSend = true;
                maxTime = Time.time + maxWaitTime;
            }

            else
            {
                errorText.gameObject.SetActive(true);
                errorText.text = "incorrect format Password or Account !";
                isLogining = false;
            }
            
        }   
        
    }

    // 本地用户密码检测
    public bool check(string name,string password)
    {
        // 此练习只做长度检测



        ///规定用户只能英文(4 - 16) 规定密码只能由英文或数字组成(4 - 16)
        if (name.Length < 4 || name.Length > 16 || password.Length < 4 || password.Length > 16)
        {
            return false;
        }
        return true;
    }

}
