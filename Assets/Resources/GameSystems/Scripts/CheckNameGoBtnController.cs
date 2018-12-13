using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckNameGoBtnController : MonoBehaviour {
    public Text UserNameValueText;
    public InputField InputUserName;
    public Text BadNameText;
    public GameObject InputNameBg;
    public GameObject Player;                   // 玩家对象
    public GameObject enemys;                   // 敌人对象
    public GameObject dealthCermera;            // 上帝摄像机
   


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        string UserName = InputUserName.text.ToString();
        //目前名字合法性只做长度和包含空格检测... 
        if (UserName.Length < 1 || UserName.Length > 8 || UserName.Contains(" "))
        {
            //长度不符提示
            BadNameText.gameObject.SetActive(true);
            return;
        }
        //设置用户名文本
        UserNameValueText.text = UserName;
        //关闭输入用户名背景
        InputNameBg.SetActive(false);
        //激活玩家
        Player.gameObject.SetActive(true);
        // 关闭上帝摄像机
        dealthCermera.gameObject.SetActive(false);
        //激活敌人
        enemys.gameObject.SetActive(true);

    }
}
