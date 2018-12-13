using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************
 
    @Description   :   敌人发动攻击控制器
    @Version       :   1.0 
    @Author        :   Dang
    @Date          :   2018.12.2

 *********************************/

public class EnemyAttackController : MonoBehaviour {

    public float attackCD;                      // 攻击冷却时间
    float LessAttackCD;                         // 剩余冷却时间 
    public float attackDemage;                  // 攻击伤害
    public GameObject player;                   // 玩家游戏对象
    bool isCanAttack;                           // 是否能进行攻击
    Animator enemyAnim;                         // 敌人动画
   

	// Use this for initialization
	void Start () {
        LessAttackCD = 0;
	}
	
	// Update is called once per frame
	void Update () {
        // 刷新剩余冷却时间
        if (LessAttackCD > 0)
        {
            LessAttackCD -= Time.deltaTime;
        }
        // 可进行攻击
        else if (isCanAttack)
        {
            Attack(player);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        // 进入攻击范围
        
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("can");
            isCanAttack = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 离开进攻范围
        if (other.gameObject == player)
        {
            isCanAttack = false;
        }
    }

   

    // 攻击
    public void Attack(GameObject gameObject)
    {
        if (gameObject.CompareTag("Player"))
        {
            //Debug.Log("attack");
            gameObject.GetComponent<PlayerHealthController>().Attacked(attackDemage);
            
        }
 
        // 进入冷却
        LessAttackCD = attackCD;
    }
}
