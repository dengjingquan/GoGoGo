using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointed : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {

    public float initX;
    public float initY;
    public float initZ;
    public float pointedX;
    public float pointedY;
    public float pointedZ;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.Find("BtnImage").transform.localScale = new Vector3(pointedX, pointedY, pointedZ);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.Find("BtnImage").transform.localScale = new Vector3(initX, initY, initZ);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
