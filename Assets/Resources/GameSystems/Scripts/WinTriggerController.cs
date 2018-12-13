using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTriggerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(00, 45*Time.deltaTime, 0, Space.World);
	}

    public void OnTriggerEnter(Collider other)
    {
        // 玩家触碰 游戏结束
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<GameOverController>().GameOver(true);
        }
    }
}
