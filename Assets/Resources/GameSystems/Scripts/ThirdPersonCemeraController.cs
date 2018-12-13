using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************
 @Description   :   第三人称摄像机控制器
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.12.7
********************************/

public class ThirdPersonCemeraController : MonoBehaviour {
    
    public GameObject player;           // 玩家对象
    public Vector3 distance;            // 摄像机与玩家距离
    public float sensitivityHor;        // 镜头（角色）转向灵敏度
    public float sensitivityVert;       // 镜头（角色）转向灵敏度
    public float maxXEuler = 0;         // 绕X轴最大欧拉角
    public float minXEuler = 70;        // 绕X轴最小欧拉角
  
	// Use this for initialization
	void Start () {
        // 摄像机与玩家初始距离
        distance = this.transform.position - player.transform.position;
        // 灵敏度
        sensitivityHor = MouseSensitivityController.sensitivityHor;
        sensitivityVert = MouseSensitivityController.sensitivityVert;

	}
	
	// Update is called once per frame
	void Update () {  
        // 刷新距离
        this.transform.position = player.transform.position + distance;          
	}

    private void LateUpdate()
    {
        
        //获取鼠标滑动 
        
        float mouseY = Input.GetAxis("Mouse Y");
        if (mouseY != 0)
        {
            
            float eulerAnglesX = transform.eulerAngles.x;
            //Debug.Log(eulerAnglesX);
            if ( (mouseY > 0 && eulerAnglesX < 270) || (mouseY < 0 && (eulerAnglesX < 70||eulerAnglesX > 340)))
            {
                transform.RotateAround(player.transform.position, this.transform.right, -mouseY * sensitivityVert);
            }
        }
        // 围绕玩家旋转

    
        float mouseX = Input.GetAxis("Mouse X");
        if (mouseX != 0)
        {
            // 围绕玩家旋转
            transform.RotateAround(player.transform.position, player.transform.up, mouseX * sensitivityHor);
        }
        // 刷新距离向量  
        distance = (this.transform.position - player.transform.position);
    }
}
