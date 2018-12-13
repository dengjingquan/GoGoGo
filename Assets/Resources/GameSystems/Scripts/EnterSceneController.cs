using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*********************************
 @Description   :   进入场景图片控制器
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.12.2
********************************/

public class EnterSceneController : MonoBehaviour {
    public Color color;

	// Use this for initialization
	void Start () {
        color = this.GetComponent<Image>().color;
        color.a = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (color.a > 0)
        {
            color = new Color(1,1,1,color.a - Time.deltaTime*1);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        
	}
}
