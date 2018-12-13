using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*********************************
 @Description   :   玩家生命显示控制器
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.12.2
********************************/

public class PlayerHealthDisplayController : MonoBehaviour {

    public GameObject Player;       // 玩家对象
    public Slider healthSlider;     // 生命显示滑动条对象
    public Text healthText;         // 生命显示文本
    // Use this for initialization
    float maxHealth;
    float currHealth;
	void Start () {
        maxHealth = Player.GetComponent<PlayerHealthController>().maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        // 获取当前生命值
        currHealth = Player.GetComponent<PlayerHealthController>().currHealth;
        if (currHealth > 0)
        {
            // 更新滑动条Value
            healthSlider.value = currHealth / maxHealth;
            healthText.text = ((int)currHealth).ToString();    
        }
         
	}

    
}
