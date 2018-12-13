using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*********************************
 @Description   :   玩家移动控制器
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.11.30
********************************/

public class PlayerMoveController : MonoBehaviour
{
    public float speed = 5f;            
    Vector3 movement;                   
    Animator anim;                      
    //Rigidbody playerRigidbody;          
    public float gravity = 1000;
    private CharacterController charController;
    public float sensitivityHor;
    public float sensitivityVert;
    public float minimumVert = -15.0f;
    public float maximumVert = 45.0f;
    private Vector3 moveDirection = Vector3.zero;
    bool isTwice = false;


    void Awake()
    {
        // Set up references.
        anim = GetComponent<Animator>();
        //playerRigidbody = GetComponent<Rigidbody>();
        sensitivityHor = MouseSensitivityController.sensitivityHor;
        
    }

    private void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    private void LateUpdate()
    {
        
    
    }

    private void Update()
    {
       // 水平转向
       transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
       

    }

    void FixedUpdate()
    {


        if (charController.isGrounded)
        {

            float deltaX = Input.GetAxis("Horizontal");
            float deltaZ = Input.GetAxis("Vertical");
            moveDirection = new Vector3(deltaX, 0, deltaZ);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            Animating(deltaX, deltaZ);
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = 12;
                isTwice = false;

            }


        }
        else
        {
            if (!isTwice && Input.GetButtonDown("Jump"))
            {
                // 释放二段跳跃
                Debug.Log("in");
                moveDirection.y = 14;
                isTwice = true;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime*2.5f;
        charController.Move(moveDirection * Time.deltaTime);
    
       
    }


    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}


