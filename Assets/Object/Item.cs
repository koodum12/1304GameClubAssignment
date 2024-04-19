using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type {Skill1,HpCube,Coin}//아이템 종류 구분

    public Type type;

    public int value;

    private Rigidbody myRigid;
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigid.MoveRotation(myRigid.rotation*Quaternion.Euler(Vector3.up*0.5f));
    }
}
