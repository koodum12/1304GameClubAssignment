using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Level1EnemyScripts : MonoBehaviour
{
    public GameObject Enemy;
    private BoxCollider EnemyBoxCollider;
    private Rigidbody EnemyRigidbody;
    public Material Enemycolor;
    public Material EnemycolorTemp;
    public float Enemy1Hp;
    public BoxCollider gloveMouse1Check;
    public Transform target;
    public GameObject taget0;
    private NavMeshAgent nav;
    public int Level1KillCount=0;
    
    void Start()
    {
        EnemyBoxCollider = GetComponent<BoxCollider>();
        EnemyRigidbody = GetComponent<Rigidbody>();
        Enemycolor = GetComponent<MeshRenderer>().material;
        EnemycolorTemp = GetComponent<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();


    }

    void Update()
    {

        if(target != null)
         nav.SetDestination(target.position);
        if (Enemy1Hp <= 0)
        {
            Debug.Log("YESSSSSSSSSss");
            Destroy(Enemy);
            Level1KillCount++;
        }
        else
        {
            Debug.Log("Hummmmmmmm");
        }
    }

    private void FixedUpdate()
    {
       
    }
    void OnTriggerEnter(Collider other)
    {
        GloveDmg1();
        Debug.Log("OnTrigger");
    
        if (other.tag == "Glove")
        {
            Weapon weapon = other.GetComponent<Weapon>();

            
            Enemy1Hp -= 10;
            Debug.Log(Enemy1Hp);
            Debug.Log("Gloves!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            GloveDmg1();
            
            
        }
    }


    void GloveDmg1()
    {
        Debug.Log("I'm come here"); ;
        Enemycolor.color =Color.red ;
        Debug.Log("아프다~~~~~~~~~~~~~~~~~~");
        StartCoroutine(scarecroHitColor());
    }

    IEnumerator scarecroHitColor()
    {
        Debug.Log("Coroutine");
        float cool=0.5f;
        while (cool >= 0)
        {
            yield return new WaitForSeconds(0.125f);
            cool -= 0.1f;
        }

        Enemycolor = EnemycolorTemp;
    }

    void OnEnable()
    {
        if(target == null)
            target = GameManager.instance.player.GetComponent<Transform>();
    }
}