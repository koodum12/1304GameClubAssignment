using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scarecrow : MonoBehaviour
{
    private BoxCollider scarecrowBoxCollider;
    private Rigidbody scarecrowRigidbody;
    public Material scarecrowhitcolor;
    public float scarecrowHp;
    public BoxCollider gloveMouse1Check;
    
    void Start()
    {
        scarecrowBoxCollider = GetComponent<BoxCollider>();
        scarecrowRigidbody = GetComponent<Rigidbody>();
        scarecrowhitcolor = GetComponent<MeshRenderer>().material;
        
        

    }

    void Update()
    {
        
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

            
                scarecrowHp -= 10;
                Debug.Log("GloveDmg:" + 10);
                Debug.Log("Gloves!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                GloveDmg1();
            
            
        }
    }


    void GloveDmg1()
    {
            Debug.Log("I'm come here");
            
            scarecrowhitcolor.color = Color.red;
            Debug.Log("아프다~~~~~~~~~~~~~~~~~~");
            StartCoroutine(scarecroHitColor());
    }

    IEnumerator scarecroHitColor()
    {
        Debug.Log("Coroutine");
        float cool=0.5f;
        while (cool <= 0)
        {
            yield return new WaitForSeconds(0.125f);
        }

        scarecrowhitcolor.color = Color.white;
    }
}