using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemysController : MonoBehaviour {

    public Vector3[] enemyInitPosition;         // 敌人放置到四个角落坐标
    GameObject bearPrefab;                      // 预制件
    GameObject elephantPrefab;                  // 预制件
    GameObject bunnyPrefab;                     // 预制件
    float nowTime;                              // 当前时间
    public float timeBreak = 30;                // 产生怪物时间间隔
    public int enemyLevel = 1;                  // 怪物等级                   
    public int enemyNumPerTime = 12;            // 每次产生怪物数量

	// Use this for initialization
	void Start () {
        //加载敌人预制件
        nowTime = Time.time;
        bearPrefab = (GameObject)Resources.Load("GameSystems/Prefabs/ZomBear");
        elephantPrefab = (GameObject)Resources.Load("GameSystems/Prefabs/ZomBunny");
        bunnyPrefab = (GameObject)Resources.Load("GameSystems/Prefabs/Hellephant");
        ProduceEnemys(enemyLevel);
    }
	
	// Update is called once per frame
	void Update () {
        // 时间间隔大于产生
		if (Time.time - nowTime > timeBreak)
        {
            nowTime = Time.time;
            ProduceEnemys(enemyLevel); 
            
        }
    }

    void ProduceEnemys(int level)
    {
        for (int j = 0; j < 4; j++)
        {
            // 加载
            GameObject bear = Instantiate(bearPrefab,enemyInitPosition[j],new Quaternion(0,0,0,0));
            //bear.SetActive(true);
            bear.transform.parent = this.transform;
            //bear.transform.localScale = new Vector3(1, 1, 1);
            // 基于等级增加生命
            // bear 攻击双倍增加
            bear.GetComponent<EnemyHealthController>().maxHealth += (level * 20);
            bear.GetComponent<EnemyHealthController>().currHealth = bear.GetComponent<EnemyHealthController>().maxHealth;
            bear.GetComponent<EnemyAttackController>().attackDemage += (level * 5) * 2;
            bear.GetComponent<NavMeshAgent>().speed += (1 * level);
            // 此处并未实现从四个角落生成，无论坐标如何调试怪物总是在一个地方诞生（已解决） 
            // 上述解决方法为实例化时定义好位置
            // bear.GetComponent<Transform>().localPosition = new Vector3(enemyInitPosition[j].x, enemyInitPosition[j].y, enemyInitPosition[j].z);
           

            // 大象生命双倍增加
            GameObject elephant = Instantiate(elephantPrefab, enemyInitPosition[j], new Quaternion(0, 0, 0, 0));
            elephant.transform.parent = this.transform;
           
            elephant.GetComponent<EnemyHealthController>().maxHealth += (level * 20) * 2;
            elephant.GetComponent<EnemyHealthController>().currHealth = elephant.GetComponent<EnemyHealthController>().maxHealth;
            elephant.GetComponent<EnemyAttackController>().attackDemage += (level * 5);
            elephant.GetComponent<NavMeshAgent>().speed += (1 * level * 2);
            // elephant.GetComponent<Transform>().localPosition = new Vector3(enemyInitPosition[j].x, enemyInitPosition[j].y, enemyInitPosition[j].z);
        
            // bunny移速双倍增加
            GameObject bunny = Instantiate(bunnyPrefab, enemyInitPosition[j], new Quaternion(0, 0, 0, 0));
            bunny.transform.parent = this.transform;
            bunny.GetComponent<EnemyHealthController>().maxHealth += (level * 20);
            bunny.GetComponent<EnemyHealthController>().currHealth = bunny.GetComponent<EnemyHealthController>().maxHealth;
            bunny.GetComponent<EnemyAttackController>().attackDemage += (level * 5);
            bunny.GetComponent<NavMeshAgent>().speed += (1 * level * 2);
            // bunny.GetComponent<Transform>().localPosition = new Vector3(enemyInitPosition[j].x, enemyInitPosition[j].y, enemyInitPosition[j].z);
        }

        // 怪物成长基数 + 1
        enemyLevel += 1;
    }
}
