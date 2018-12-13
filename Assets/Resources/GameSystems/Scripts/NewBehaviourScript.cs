using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;

public class NewBehaviourScript : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler{
    public Animation change;
    public Image image;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // change.Play("Image(1)");
        change.Play();
        Debug.Log("in");
        
        //throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //GetComponent<Animator>().StopPlayback();
        // change.StopPlayback();
        //throw new System.NotImplementedException();
        change.Stop();
        Debug.Log("out");
        
    }

    // Use this for initialization
    void Start () {
        change = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
            
    }

 

    
}
