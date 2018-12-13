using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/*********************************
 @Description   :   玩家生命控制器
 @Version       :   2.0 
 @Author        :   Dang
 @Date          :   2018.12.11
********************************/

public class PlayerHealthController : MonoBehaviour {
    public float maxHealth;         // 最大生命值        
    public float currHealth;        // 当前生命值
    bool isDead;                    // 是否死亡
    public GameObject deathCermera; // 上帝相机

  


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        // 角色死亡
        if (currHealth <= 0)
        {    
            GetComponent<GameOverController>().GameOver(false);

        }
    }



    // 遭受攻击
    public void Attacked(float demage)
    {
        if (currHealth > 0)
        {
            currHealth -= demage;
        }
    }
}
