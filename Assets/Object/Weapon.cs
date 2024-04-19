using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    public TrailRenderer trailRendererGlove;
    public GameObject Glove;
    public Animator GloveAnimator;
    public int GloveAnimationSpeed = 3;
    public float Glove1Damage=3;
    public bool GloveCollider;
    
    public GameObject assistant_glove;
    public GameObject assistant_glove_1;
    public GameObject assistant_glove_2;
    public GameObject assistant_glove_3;
    
    public float glovecuntinuityCooltime;
    private bool glovecuntinuityCanUse=true;
    public float glovecuntinuityUsetime;

    public int gloveClick1Use=0;

    public BoxCollider gloveRightBoxCollider;
    public enum Type {meleeWeapon, rangedWeapon};

    public Type type;
    void Start()
    {
        GloveAnimator = GetComponentInChildren<Animator>();
        GloveAnimator.speed = GloveAnimationSpeed;
        gloveRightBoxCollider.enabled = false;
    }
    void Update()
    {
        if (type == Type.meleeWeapon)
        {
            Glove_attack();
        }
    }

        
    void Glove_attack()
    {
        if (Input.GetMouseButtonDown(0)&&gloveClick1Use==0)
        {
                
                GloveAnimator.SetInteger("gloveClick0",1);  
                gloveRightBoxCollider.enabled = true;
                gloveClick1Use = 1;
                StartCoroutine(GloveClick0());
        }
        
        if (Input.GetKeyDown(KeyCode.E)&&glovecuntinuityCanUse)
        {
                    Debug.Log("Assistant");
                    GloveAnimator.SetBool("continuityPunch",true);
                    assistant_glove.SetActive(true);
                    assistant_glove_1.SetActive(true);
                    assistant_glove_2.SetActive(true);
                    assistant_glove_3.SetActive(true);
                    glovecuntinuityCanUse = false;
                    StartCoroutine(GlovecuntinuityCool(glovecuntinuityCooltime));
                   
        }
                
    }
    IEnumerator GloveClick0()
        {
            float cool = 2f;
            while (cool >= 0f)
            {
                yield return new WaitForSeconds(0.1f);
                cool -= 0.1f;
                if (Input.GetMouseButtonDown(0)&&gloveClick1Use==1)
                {
                    GloveAnimator.SetInteger("gloveClick0",2);
                    break;
                }
            }
                         
            gloveClick1Use = 0; 
            GloveAnimator.SetInteger("gloveClick0",0);
            
        }
    IEnumerator GlovecuntinuityCool(float cool)
    {
        //GloveAnimator.SetBool("continuityPunch",false);
        while(cool>=0){
        yield return new WaitForSeconds(0.1f);
        if (cool < glovecuntinuityCooltime - glovecuntinuityUsetime)
        {
            GloveAnimator.SetBool("continuityPunch",false);
            assistant_glove.SetActive(false);
            assistant_glove_1.SetActive(false);
            assistant_glove_2.SetActive(false);
            assistant_glove_3.SetActive(false);
        }
        cool-=0.1f;
        }
        glovecuntinuityCanUse=true;
    }
}
