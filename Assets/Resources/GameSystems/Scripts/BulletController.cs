using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************
 @Description   :   子弹控制器
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.12.2
********************************/

public class BulletController : MonoBehaviour {

    public float bulletForce;
    public GameObject Player;

	// Use this for initialization
	void Start () {
        this.transform.localPosition = Player.GetComponent<Transform>().localPosition;
        this.transform.localRotation = Player.GetComponent<Transform>().localRotation;
        GetComponent<Rigidbody>().AddForce(Vector3.forward*bulletForce);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Debug.Log("yse");
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
