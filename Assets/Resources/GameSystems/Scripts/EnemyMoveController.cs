using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*********************************
 @Description   :   控制敌人巡逻，移动
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.12.8
********************************/

public class EnemyMoveController : MonoBehaviour {
    GameObject player;                               // 游戏玩家.
    NavMeshAgent nav;                                // 导航网格代理（寻路）.
    public Vector3[] patrolPositions;                // 巡逻坐标
    int patrolPositionsSize;                         // 坐标数量
    int currPosition = 0;                            // 当前目的坐标
    public float patrolSpeed = 2;                    // 巡逻速度 根据怪物种类不同 该速度不同 
    public float attackSpeed = 3.5f;                 // 追击速度 根据怪物种类不同 该速度不同
    bool isAttackPlayer;                             // 是否察觉玩家
    public float distance = 10f;                     // 察觉玩家距离 根据怪物种类不同 该距离不同 默认10


    private void Awake()
    {
        // 随机出现在一个位置
        patrolPositionsSize = patrolPositions.Length;
        currPosition = (int)(Random.Range(0, patrolPositionsSize - 0.1f));
        //Debug.Log(currPosition);
        this.transform.position = patrolPositions[currPosition];
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        // 初始化为非追击状态
        isAttackPlayer = false;
        nav.SetDestination(patrolPositions[currPosition]);   
    }


    void Update()
    {
       
        // 更新是否可追击状态
        if ( Vector3.Distance(transform.position, player.transform.position) < distance)
        {
            // 更新状态
            isAttackPlayer = true;
            // 更新速度
            nav.speed = attackSpeed;
        }

        // 追击状态
        if (isAttackPlayer)
        {
            nav.SetDestination(player.transform.position);
        }
        else
        {
            // 到达目的地附近 设置下一个随机目标点
            if( Vector3.Distance(transform.position ,patrolPositions[currPosition]) < 3 )
            {
                currPosition = (int)(Random.Range(0, patrolPositionsSize - 0.1f));
                nav.SetDestination(patrolPositions[currPosition]);
            }
            
        }
    }
}
