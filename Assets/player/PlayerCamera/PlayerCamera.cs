using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject player;//카메라가 따라다닐 타겟(플레이어)
    public float offsetX = 0f;//카매라 x좌표
    public float offsetY = 0f;//카메라 y좌표
    public float offsetZ = 0f;//카메라 z좌표
    private float tempX;
    private float tempY;
    private float tempZ;
    private bool View_1 = false;
    public bool View_1Check;
    
    [SerializeField]
    public float cameraSpeed;
    
    private Vector3 playerPos; // 플레이어 위치
    private float rotationX;
    private float rotationY;
    public float rotationSpeed;
    private Rigidbody myRigid;


    private void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R)&&!(View_1))
        {
            tempX = offsetX;
            tempY = offsetY;
            tempZ = offsetZ; 
            offsetX = -1f;
            offsetY = 1f;
            offsetZ = 0f;
            View_1 = true;
            View_1Check = true;
            Debug.Log("R");

        }else if(Input.GetKeyUp(KeyCode.R)&&View_1)
        {
            offsetX = tempX;
            offsetY = tempY;
            offsetZ = tempZ;
            View_1 = false;
            View_1Check = false;
            Debug.Log("r");
            
        }
    }

    private void FixedUpdate()//Update보단 FIxedUpdate 가 더 효율이 좋다고 함 (암튼 블로그에서 그럼)
    {
        PlayerRotation();
        playerPos = new Vector3(//playerPos = new Vector(x,y,z,)구조 enter은 그냥 보기 편하게 하려고
            player.transform.position.x + offsetX,
            player.transform.position.y + offsetY,
            player.transform.position.z + offsetZ
        );
        transform.position = Vector3.Lerp(transform.position, playerPos, cameraSpeed * Time.deltaTime);
    }

    void PlayerRotation()
    {
        
    }
}
