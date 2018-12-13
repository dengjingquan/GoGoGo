using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitUI : MonoBehaviour
{

    public GameObject loginUI;
    public GameObject startGameUI;

    // Use this for initialization
    void Start()
    {
        
        if (GameSocket.isOnline)
        {
            startGameUI.SetActive(true);
            loginUI.SetActive(false);
        }
        else
        {
            startGameUI.SetActive(false);
            loginUI.SetActive(true);
        }

    }
}
