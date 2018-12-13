using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitBtnController : MonoBehaviour {

	// Quit Game
    public void OnClick()
    {
        Debug.Log("exit");
        GameSocket.streamToServer.Close();
        GameSocket.client.Close();
        Debug.Log("duankai");
        Application.Quit();
    }
}
