using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************
 @Description   :   敌人攻击控制器
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.12.2
********************************/

public class ShootColliderController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionExit(Collision collision)
    {
        this.gameObject.SetActive(false);
    }


}
