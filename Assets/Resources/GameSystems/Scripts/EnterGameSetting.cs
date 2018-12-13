using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Scripting;

public class EnterGameSetting : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler{

    public Animation pointedAnimation;
    public Image btnBgImg;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        SceneManager.LoadScene("Scenes/minigame");
       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.Find("Start Image").gameObject.SetActive(false);
        GetComponent<Image>().gameObject.SetActive(true);
        pointedAnimation.Play();
        //transform.Find("Start Image").gameObject.i
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.Find("Start Image").gameObject.SetActive(true);
        pointedAnimation.Stop();
        GetComponent<Image>().gameObject.SetActive(false);
        //transform.Find("Start Image").gameObject.SetActive(true);
    }
}
