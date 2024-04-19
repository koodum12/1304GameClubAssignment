using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.XR;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float walkSpeed;//플레이어 움직임 속도(기본값)
    private Vector3 move;//플래이어 움직임 방향
    [SerializeField] public float rotateSpeed; //캐릭터 회전 속도
    public Camera camera;
    public GameObject player;
    
    //-----------------------------------------------------------------------------------
    [SerializeField] public float jumpPower;//점프 높이
    private bool stayground;//지표면 확인
    private BoxCollider boxCollider;
    //----------------------------------------------------------------------------------
    private Rigidbody myRigid;
    [SerializeField] public float dashSpeed;//shift 눌렀을 시 순간 속도 증가
    public float dashSpeedTemp=1f;//dashSpeed 복사
    public bool dashCanUse = true;//대쉬 사용 가능 여부
    [SerializeField]
    public float dashCoolTime;//대쉬 쿨타임
    //----------------------------------------------------------------------------------
    private GameObject nearObject;//근처에 있는 오브젝트(collider 충돌 여부로 판단).
    public GameObject[ ]Skill;//Skill이라는 태그를 가진 오브젝트
    private int skillIndex;
    public GameObject[] HpCube;
    private float HpCube20=20;
    public bool[] hasSkill;
    private bool swap1;
    public bool view_1;

    public float playerHp;
    public float playerHpTemp;
    public float Dmg;
    

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        playerHpTemp = playerHp;
    }

    // Update is called once per frame
    void Update()
    {
        StayGround();
        Swap();
        view_1 = camera.GetComponent<PlayerCamera>().View_1Check;
        Die();
    }

    void Die()
    {
        if (playerHp <= 0)
        {
            Destroy(player);
        }
    }
    private void FixedUpdate()
    {
        DashTry();
        Move();
        JumpTry();
        GetItem();
        
    }
    void StayGround()
     {
        stayground = Physics.Raycast(transform.position, Vector3.down,boxCollider.bounds.extents.y + 0.1f);
        //Physics.Raycast:충돌체 감지 
     }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        move = new Vector3(x, 0, z).normalized;
        transform.position += move *(walkSpeed * Time.deltaTime);
        if (!(x == 0 && z == 0))
        {
            transform.position += move *(walkSpeed *(Time.deltaTime*dashSpeedTemp));
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(move),
                Time.deltaTime * rotateSpeed);
            if (view_1 == true)
            {
                camera.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(move),
                    Time.deltaTime * rotateSpeed);
            }

            if (view_1 == false)
            {
                camera.transform.eulerAngles = new Vector3(90f, 0, 0);
            }
        }

    }

    void DashTry()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)&&dashCanUse)
        {
            Dash();
            StartCoroutine(DashCool(0));
            
        }
    }

    void Dash()
    {
        dashSpeedTemp = dashSpeed;
        

    }

    IEnumerator DashCool(float cool)
    {
        while (cool <= dashCoolTime)
        {
            yield return new WaitForSeconds(0.1f);
            if (cool >= 0.2)
            {
                Debug.Log("true");
                DashEnd();
            }

            cool+=0.1f;
        }
        
        dashCanUse = true;
    }

    void DashEnd()
    {
        dashCanUse = false;
        dashSpeedTemp = 1f;
    }
    void JumpTry()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&stayground)
        {
            myRigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

    
    /*void GetItem()
    {
        if (gameObject != null)
        {
            if (nearObject.tag == "Skill")
            {
                
            }
        }
    }*/
    void GetItem()
    {
        if (nearObject != null)//만약 다른 오브젝트와 붙어 있다면
        {
            if (nearObject.tag == "Skill")//만약 붇어 있는 오브젝트의 태그가 Skill이라면
            {
                Item item = nearObject.GetComponent<Item>();
                int SkillIndex = item.value;
                hasSkill[SkillIndex] = true;
                Destroy(nearObject);
            }

            if (nearObject.tag == "HpCube")//만약 붙어 있는 오브젝트의 태그가 HpCube라면
            {
                Destroy(nearObject);
                if (!(playerHp+HpCube20>=playerHpTemp))
                {
                    playerHp += 10;
                }else if (playerHp < playerHpTemp)
                {
                    playerHp += playerHpTemp - playerHp;
                }
            }
        }
    }

    void Swap()
    {
        swap1 = Input.GetButtonDown("Swap1");
        if (swap1&&hasSkill[0]==true)
        {
            Skill[skillIndex].SetActive(false);
            skillIndex = 0;
            Skill[skillIndex].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "killGround")//만약 KillGround라는 태그와 붇어있다면
        {
            transform.position = new Vector3(0, 40, -35);
            playerHp -= 5;
        }

        
    }

    private void OnTriggerStay(Collider other)
    {
        if (gameObject.tag == "player" &&other.tag == "Level1Enemy")
        {
                 
                 playerHp -= 10;
                 Debug.Log("playerHp"+playerHp);
        }
     
        if (other.tag == "Level2Enemy")
        {
            
                 playerHp -= 20;
                 Debug.Log("playerHp"+playerHp);
        }

        if (other.tag == "Bullet")
        {
            Debug.Log("히히 발싸!");
            playerHp -= 5;
        }
        if (other.tag == "Skill")//만약 Skill이라는 태그를 가진 오브젝트와 접촉한다면
        {
            nearObject = other.gameObject;
            
        }
        if (other.tag == "HpCube")//만약 HpCube라는 태그와 접촉한다면
        {
            nearObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Skill")//만약 Skill이라는 태그와 떨어진다면
        {
            nearObject = null;
        }
        if (other.tag == "HpCube")//만약 HpCube라는 태그와 떨어진다면
        {
            nearObject = null;
        }
    }
}
