using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*********************************
 @Description   :   敌人生命控制器
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.12.2
********************************/

public class EnemyHealthController : MonoBehaviour {
    public float maxHealth;                     // 最大生命值.
    public float currHealth;                    // 当前生命值.
    public int scoreValue = 10;                 // 敌人分值
    public AudioClip deathClip;                 // 敌人死亡音效.
    Animator enemyAnim;                         // 敌人动画.
    AudioSource enemyAudio;                     // 敌人音效.
    public ParticleSystem attackedParticles;    // 受到伤害粒子系统
    public ParticleSystem deadParticles;        // 死亡粒子系统
    CapsuleCollider capsuleCollider;            // 碰撞器.

    void Start()
    {
        //attackedParticles = GetComponent<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyAnim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(currHealth < 0)
        {
            Death();
        }
    }

    // 受到攻击
    public void Attacked(float demage, Vector3 hitPoint)
    {
        if (currHealth > 0)
        {
            // 播放受伤音频
            enemyAudio.Play();

            // 刷新当前生命值.
            currHealth -= demage;

            // 粒子系统Position.
            attackedParticles.transform.position = hitPoint;

            // 受伤粒子系统.
            attackedParticles.Play();

            // 生命值不大于0死亡
            if (currHealth <= 0)
            {
                Death();
            }
        }
    }


    void Death()
    {
     
        // 关闭触发器.
        capsuleCollider.isTrigger = false;
        // 播放死亡动画.
        enemyAnim.SetTrigger("Dead");
        // 播放死亡音频.
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        
    }

    // 死亡动画回调事件 销毁对象
    public void StartSinking()
    {
        Destroy(gameObject, 2f);
    }
}
