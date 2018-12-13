using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************
 @Description   :   断开TCP连接 避免在开发过程直接通过游戏关闭导致第二次运行死机
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.12.2
********************************/

public class DisconnectTCPController : MonoBehaviour {

    // 游戏结束时断开
    private void OnApplicationQuit()
    {
       // Debug.Log("disconnect");
        if (GameSocket.client != null)
        {
            GameSocket.streamToServer.Close();
            GameSocket.client.Close();
        }
    }
}
